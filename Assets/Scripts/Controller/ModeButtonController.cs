using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used by the Control Mode Button
/// </summary>
public class ModeButtonController : MonoBehaviour
{
    public CameraController Camera;
    public Sprite CompassSprite, ArrowsSprite;

    Image _image;

    // Use this for initialization
    void Start()
    {
        _image = GetComponent<Image>();

        if (SystemInfo.deviceType == DeviceType.Handheld && SystemInfo.supportsGyroscope)
            SetImageSource();
        else
            _image.enabled = false;
    }

    /// <summary> Toggle the control mode between gyroscope controls and touch controls </summary>
    public void ToggleMode()
    {
        Camera.Mode = Camera.Mode == ControlMode.Gyro ? ControlMode.Touch : ControlMode.Gyro;
        Camera.SetCameraParentRotation(Camera.CurrentOrientation, Camera.CurrentOrientation);
        SetImageSource();
    }

    /// <summary> Set the 'toggle modes' button image </summary>
    public void SetImageSource()
    {
        _image.sprite = Camera.Mode == ControlMode.Gyro ? ArrowsSprite : CompassSprite;
    }
}
