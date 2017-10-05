using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    enum ControlMode
    {
        Gyro,
        Touch,
        Mouse
    };

    public Sprite CompassSprite, ArrowsSprite;
    public Image ModeButton;

    ControlMode _mode;
    Quaternion _origRotation;
    Gyroscope _gyro;
    float _mouseSensitivity = 70f;
    float _touchSensitivity = 2.0f;
    float _rotationX = 0.0f;
    float _rotationY = 0.0f;
    float _velocityX, _velocityY;
    float _deceleration = 1.1f;

    // Initialize controller
    void Start()
    {
        // Set the starting control mode
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            _mode = ControlMode.Mouse;
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld && SystemInfo.supportsGyroscope)
        {
            _mode = ControlMode.Gyro;

            _gyro = Input.gyro;
            _gyro.enabled = true;
        }
        else
        {
            _mode = ControlMode.Touch;
        }

        _origRotation = transform.rotation;

        // Get the button for switching modes
        if (SystemInfo.deviceType != DeviceType.Handheld || !SystemInfo.supportsGyroscope)
            ModeButton.enabled = false;
        else
            SetImageSource();

        Input.compass.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mode == ControlMode.Mouse)
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
        else if (_mode == ControlMode.Touch)
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
        else if (_mode == ControlMode.Gyro)
        {
            // Need to invert gyroscope z and w axis, and rotate by 90 degrees on the x axis
            Quaternion rotate = new Quaternion(_gyro.attitude.x, _gyro.attitude.y, -_gyro.attitude.z, -_gyro.attitude.w);
            transform.rotation = Quaternion.Euler(90, 0, 0) * rotate;

            _rotationX = transform.rotation.eulerAngles.y;
            _rotationY = -transform.rotation.eulerAngles.x;
        }
    }

    /// <summary> Rotates the camera by the X and Y velocity </summary>
    void RotateCamera()
    {
        _rotationX -= _velocityX;
        _rotationY -= _velocityY;
        _rotationY = Mathf.Clamp(_rotationY, -90, 90);

        transform.rotation = _origRotation;
        transform.rotation *= Quaternion.AngleAxis(_rotationX, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(_rotationY, Vector3.left);
    }

    /// <summary> Decreases the X and Y velocity, and rotates the camera </summary>
    void DecelerateCamera()
    {
        bool move = false;

        // Get the new speed (without direction), after deceleration
        var newVelX = Mathf.Abs(_velocityX) / _deceleration;

        // If the speed is positive, the camera will move
        if (newVelX > 0)
        {
            // Get the velocity (with direction)
            if (_velocityX < 0)
                newVelX *= -1;
            _velocityX = newVelX;

            move = true;
        }
        else // If the speed is negative or 0, set the velocity to 0
            _velocityX = 0;

        // Do the same for the Y axis
        var newVelY = Mathf.Abs(_velocityY) / _deceleration;
        if (newVelY > 0)
        {
            if (_velocityY < 0)
                newVelY *= -1;
            _velocityY = newVelY;

            move = true;
        }
        else
            _velocityY = 0;

        if (move)
            RotateCamera();
    }

    /// <summary> Toggle the control mode between gyroscope controls and touch controls </summary>
    public void ToggleMode()
    {
        _mode = _mode == ControlMode.Gyro ? ControlMode.Touch : ControlMode.Gyro;

        SetImageSource();
    }


    /// <summary> Set the 'toggle modes' button image </summary>
    public void SetImageSource()
    {
        if (_mode == ControlMode.Gyro)
            ModeButton.sprite = ArrowsSprite;
        else
            ModeButton.sprite = CompassSprite;
    }
}
