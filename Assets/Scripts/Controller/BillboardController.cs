using UnityEngine;

/// <summary>
/// Controls billboard GameObjects (Canvases that always face the main camera)
/// </summary>
public class BillboardController : MonoBehaviour
{
    Camera _camera;
    CameraController _cameraController;

    void Start()
    {
        _camera = Camera.main;
        _cameraController = _camera.GetComponent<CameraController>();
    }

    void Update()
    {
        if (_cameraController.Mode == ControlMode.Gyro)
        {
            transform.rotation = Quaternion.Euler(_camera.transform.rotation.eulerAngles.x, _camera.transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            switch (_cameraController.CurrentOrientation)
            {
                case DeviceOrientation.Portrait:
                    transform.rotation = _camera.transform.rotation;
                    break;
                case DeviceOrientation.LandscapeLeft:
                    transform.rotation = _camera.transform.rotation * Quaternion.Euler(0, 0, -90);
                    break;
                case DeviceOrientation.LandscapeRight:
                    transform.rotation = _camera.transform.rotation * Quaternion.Euler(0, 0, 90);
                    break;
                case DeviceOrientation.PortraitUpsideDown:
                    transform.rotation = _camera.transform.rotation * Quaternion.Euler(0, 0, 180);
                    break;
            }
        }
    }
}
