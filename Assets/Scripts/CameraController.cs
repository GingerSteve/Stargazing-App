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

    ControlMode _mode;
    Image _modeButton;
    Quaternion _origRotation;
    Gyroscope _gyro;
    Sprite _compass, _arrows;

    float _mouseSensitivity = 70f;
    float _touchSensitivity = 3.5f;
    float _rotationX = 0.0f;
    float _rotationY = 0.0f;


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

        // Load images
        _compass = Resources.Load<Sprite>("compass");
        _arrows = Resources.Load<Sprite>("arrows");

        // Get the button for switching modes
        _modeButton = GameObject.Find("ModeButton").GetComponent<Image>();
        if (SystemInfo.deviceType != DeviceType.Handheld || !SystemInfo.supportsGyroscope)
            _modeButton.enabled = false;
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
                //TODO lerp a little, for fast movements

                _rotationX -= Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
                _rotationY -= Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
                _rotationY = Mathf.Clamp(_rotationY, -90, 90);

                transform.rotation = _origRotation;
                transform.rotation *= Quaternion.AngleAxis(_rotationX, Vector3.up);
                transform.rotation *= Quaternion.AngleAxis(_rotationY, Vector3.left);
            }
        }
        else if (_mode == ControlMode.Touch)
        {
            if (Input.touches.Length > 0)
            {
                _rotationX -= Input.touches[0].deltaPosition.x * Time.deltaTime * _touchSensitivity;
                _rotationY -= Input.touches[0].deltaPosition.y * Time.deltaTime * _touchSensitivity;
                _rotationY = Mathf.Clamp(_rotationY, -90, 90);

                transform.rotation = _origRotation;
                transform.rotation *= Quaternion.AngleAxis(_rotationX, Vector3.up);
                transform.rotation *= Quaternion.AngleAxis(_rotationY, Vector3.left);
            }
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

    public void ToggleMode()
    {
        _mode = _mode == ControlMode.Gyro ? ControlMode.Touch : ControlMode.Gyro;

        SetImageSource();
    }

    public void SetImageSource()
    {
        if (_mode == ControlMode.Gyro)
            _modeButton.sprite = _arrows;
        else
            _modeButton.sprite = _compass;
    }
}
