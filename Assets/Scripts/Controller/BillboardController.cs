using UnityEngine;

/// <summary>
/// Controls billboard GameObjects (Canvases that always face the main camera)
/// </summary>
public class BillboardController : MonoBehaviour
{
    void Update()
    {
        var cameraController = Camera.main.GetComponent<CameraController>();

        if (cameraController.GetControlMode() != CameraController.ControlMode.Gyro)
        {
            switch (cameraController.GetOrientation())
            {
                case DeviceOrientation.Portrait:
                    transform.rotation = Camera.main.transform.rotation;
                    break;
                case DeviceOrientation.LandscapeLeft:
                    transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0, 0, -90);
                    break;
                case DeviceOrientation.LandscapeRight:
                    transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0, 0, 90);
                    break;
                case DeviceOrientation.PortraitUpsideDown:
                    transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0, 0, 180);
                    break;
            }
        }
    }
}
