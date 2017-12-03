using UnityEngine;

/// <summary>
/// Positions and rotates UI Buttons correctly depending on device rotation
/// </summary>
public class UIButtonController : MonoBehaviour
{
    public CameraController Camera;
    public ScreenPosition DefaultPosition;
    public int OffsetX;
    public int OffsetY;

    DeviceOrientation _currentOrientation;

    void Start()
    {
        _currentOrientation = Camera.CurrentOrientation;
        SetButtonPosition(_currentOrientation);
    }

    void Update()
    {
        if (_currentOrientation != Camera.CurrentOrientation)
        {
            _currentOrientation = Camera.CurrentOrientation;
            SetButtonPosition(_currentOrientation);
        }
    }

    /// <summary>
    /// Sets the position and rotation of the mode button.
    /// </summary>
    void SetButtonPosition(DeviceOrientation orientation)
    {
        var newPos = DefaultPosition;
        var rect = gameObject.GetComponent<RectTransform>();

        // Get the new position for the element
        switch (orientation)
        {
            case DeviceOrientation.Portrait:
                newPos = DefaultPosition;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case DeviceOrientation.LandscapeLeft:
                newPos = (ScreenPosition)(((int)DefaultPosition + 1) % 4);
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case DeviceOrientation.PortraitUpsideDown:
                newPos = (ScreenPosition)(((int)DefaultPosition + 2) % 4);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case DeviceOrientation.LandscapeRight:
                newPos = (ScreenPosition)(((int)DefaultPosition + 3) % 4);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }

        // Set the element's anchors and offset
        switch (newPos)
        {
            case ScreenPosition.BottomLeft:
                rect.anchorMax = new Vector2(0, 0);
                rect.anchorMin = new Vector2(0, 0);
                rect.anchoredPosition = new Vector2(OffsetY, OffsetX);
                break;
            case ScreenPosition.TopLeft:
                rect.anchorMax = new Vector2(0, 1);
                rect.anchorMin = new Vector2(0, 1);
                rect.anchoredPosition = new Vector2(OffsetX, -OffsetY);
                break;
            case ScreenPosition.BottomRight:
                rect.anchorMax = new Vector2(1, 0);
                rect.anchorMin = new Vector2(1, 0);
                rect.anchoredPosition = new Vector2(-OffsetX, OffsetY);
                break;
            case ScreenPosition.TopRight:
                rect.anchorMax = new Vector2(1, 1);
                rect.anchorMin = new Vector2(1, 1);
                rect.anchoredPosition = new Vector2(-OffsetY, -OffsetX);
                break;
        }
    }
}
