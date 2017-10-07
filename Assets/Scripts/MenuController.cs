using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Canvas ViewerCanvas;
    public Canvas MenuCanvas;
    public CameraController CameraScript;

    void Start()
    {
        MenuCanvas.enabled = false;
    }

    void Update()
    {

    }

    public void SwitchCanvas()
    {
        if (ViewerCanvas.enabled)
        {
            ViewerCanvas.enabled = false;
            CameraScript.enabled = false;
            MenuCanvas.enabled = true;
        }
        else
        {
            MenuCanvas.enabled = false;
            CameraScript.enabled = true;
            ViewerCanvas.enabled = true;
        }
    }
}
