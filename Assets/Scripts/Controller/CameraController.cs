using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the camera rotation and input
/// </summary>
public class CameraController : MonoBehaviour
{
    public ControlMode Mode { get; set; }
    public DeviceOrientation CurrentOrientation { get; private set; }

    Quaternion _origRotation;
    Gyroscope _gyro;
    GameObject _parent;
    float _rotationX;
    float _rotationY;
    float _velocityX, _velocityY;

    float _touchSensitivity;
    const float _mouseSensitivity = 70f;
    const float _deceleration = 1.1f;

    // Initialize controller
    void Start()
    {
        _touchSensitivity = 640f / Screen.dpi + 1;

        // Set the starting control mode and orientation
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Mode = ControlMode.Mouse;
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld && SystemInfo.supportsGyroscope)
        {
            Mode = ControlMode.Gyro;

            _gyro = Input.gyro;
            _gyro.enabled = true;
        }
        else
        {
            Mode = ControlMode.Touch;
        }

        // Add a parent object to the camera, for additional transforms
        _parent = new GameObject("CameraParent");
        _parent.transform.position = transform.position;
        transform.parent = _parent.transform;
        _origRotation = transform.localRotation;

        CurrentOrientation = DeviceOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.deviceOrientation != CurrentOrientation)
            SetOrientation();

        if (enabled)
        {
            if (Mode == ControlMode.Mouse)
            {
                if (Input.GetMouseButton(0))
                {
                    _velocityX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
                    _velocityY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

                    RotateCamera();
                }
                else
                    DecelerateCamera();
            }
            else if (Mode == ControlMode.Touch)
            {
                if (Input.touches.Length > 0)
                {
                    _velocityX = Input.touches[0].deltaPosition.x * Time.deltaTime * _touchSensitivity;
                    _velocityY = Input.touches[0].deltaPosition.y * Time.deltaTime * _touchSensitivity;

                    RotateCamera();
                }
                else
                    DecelerateCamera();
            }
            else if (Mode == ControlMode.Gyro)
            {
                // Need to invert gyroscope z and w axis, and rotate by 90 degrees on the x axis
                Quaternion rotate = new Quaternion(_gyro.attitude.x, _gyro.attitude.y, -_gyro.attitude.z, -_gyro.attitude.w);
                transform.localRotation = Quaternion.Euler(90, 0, 0) * rotate;

                _rotationX = transform.rotation.eulerAngles.y;
                _rotationY = -transform.rotation.eulerAngles.x;
                if (_rotationY < -90)
                    _rotationY += 360;
            }
        }
    }

    /// <summary> Rotates the camera by the X and Y velocity </summary>
    void RotateCamera()
    {
        transform.localRotation = _origRotation;

        // Because the screen orientation is locked, the input we're recieving is always in Portrait mode
        // So we have to handle things differently depending on orientation
        switch(CurrentOrientation)
        {
            case DeviceOrientation.Portrait:
                _rotationX -= _velocityX;
                _rotationY -= _velocityY;
                _rotationY = Mathf.Clamp(_rotationY, -90, 90);
                transform.Rotate(Vector3.up, _rotationX, Space.Self);
                transform.Rotate(Vector3.left, _rotationY, Space.Self);
                break;
            case DeviceOrientation.LandscapeLeft:
                _rotationX += _velocityY;
                _rotationY -= _velocityX;
                _rotationY = Mathf.Clamp(_rotationY, -90, 90);
                transform.Rotate(Vector3.left, -_rotationX, Space.Self);
                transform.Rotate(Vector3.up, _rotationY, Space.Self);
                break;
            case DeviceOrientation.LandscapeRight:
                _rotationX -= _velocityY;
                _rotationY -= _velocityX;
                _rotationY = Mathf.Clamp(_rotationY, -90, 90);
                transform.Rotate(Vector3.left, _rotationX, Space.Self);
                transform.Rotate(Vector3.up, _rotationY, Space.Self);
                break;
            case DeviceOrientation.PortraitUpsideDown:
                _rotationX += _velocityX;
                _rotationY -= _velocityY;
                _rotationY = Mathf.Clamp(_rotationY, -90, 90);
                transform.Rotate(Vector3.up, -_rotationX, Space.Self);
                transform.Rotate(Vector3.left, _rotationY, Space.Self);
                break;
        }
    }

    /// <summary> Decreases the X and Y velocity, and rotates the camera </summary>
    void DecelerateCamera()
    {
        Decelerate(ref _velocityX);
        Decelerate(ref _velocityY);

        if (_velocityX != 0 || _velocityY != 0)
            RotateCamera();
    }

    void Decelerate(ref float velocity)
    {
        // Get the new speed (without direction), after deceleration
        var newVel = Mathf.Abs(velocity) / _deceleration;

        // If the speed is positive, get the direction, and set the new velocity
        if (newVel > 0)
        {
            // Get the velocity (with direction)
            if (velocity < 0)
                newVel *= -1;
            velocity = newVel;
        }
        else // If the speed is negative or 0, set the velocity to 0
            velocity = 0;
    }

    /// <summary> Handles orientation changes </summary>
    public void SetOrientation()
    {
        var from = CurrentOrientation;
        var to = CurrentOrientation;

        // Ignore FaceUp and FaceDown rotations
        if (Input.deviceOrientation == DeviceOrientation.Portrait ||
            Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
            Input.deviceOrientation == DeviceOrientation.LandscapeRight ||
            Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            CurrentOrientation = Input.deviceOrientation;
            to = CurrentOrientation;
        }

        SetCameraParentRotation(from, to);
    }

    /// <summary>
    /// Sets the rotation of the camera's parent object.
    /// This is used to turn the camera when the orientation changes.
    /// </summary>
    void SetCameraParentRotation(DeviceOrientation from, DeviceOrientation to)
    {
        if (Mode == ControlMode.Touch)
        {
            // Rotate the parent to counter the rotation of the device
            if (to == DeviceOrientation.Portrait)
                _parent.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (to == DeviceOrientation.LandscapeLeft)
                _parent.transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (to == DeviceOrientation.LandscapeRight)
                _parent.transform.rotation = Quaternion.Euler(0, 0, -90);
            else if (to == DeviceOrientation.PortraitUpsideDown)
                _parent.transform.rotation = Quaternion.Euler(0, 0, 180);

            // Some orientation changes need the Y axis to be mirrored
            if ((from == DeviceOrientation.Portrait || from == DeviceOrientation.LandscapeLeft) &&
                (to == DeviceOrientation.LandscapeRight || to == DeviceOrientation.PortraitUpsideDown))
            {
                _rotationY = -_rotationY;
            }
            else if ((from == DeviceOrientation.PortraitUpsideDown || from == DeviceOrientation.LandscapeRight) &&
                (to == DeviceOrientation.Portrait || to == DeviceOrientation.LandscapeLeft))
            {
                _rotationY = -_rotationY;
            }

            RotateCamera();
        }
        else // Gyro mode
            _parent.transform.rotation = Quaternion.Euler(0, 0, 0);

    }
}
