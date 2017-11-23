using UnityEngine;

/// <summary>
/// Controls billboard GameObjects (Canvases that always face the main camera)
/// </summary>
public class BillboardController : MonoBehaviour
{
    Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        var cameraController = _camera.GetComponent<CameraController>();

        if (cameraController.GetControlMode() != CameraController.ControlMode.Gyro)
        {
            switch (cameraController.GetOrientation())
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
