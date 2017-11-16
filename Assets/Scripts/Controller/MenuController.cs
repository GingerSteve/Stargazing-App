﻿using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas ViewerCanvas;
    public Canvas MenuCanvas;
    public CameraController CameraScript;

    public Text NameText;
    public Image ConstImage;
    public Text DescText;
    public ScrollRect ScrollView;

    void Start()
    {
        MenuCanvas.enabled = false;
    }

    public void OpenMenu(Constellation constellation)
    {
        Screen.orientation = (ScreenOrientation)CameraScript.GetOrientation();
        Screen.orientation = ScreenOrientation.AutoRotation;

        NameText.text = constellation.Name;
        DescText.text = constellation.Description;
        ConstImage.sprite = Resources.Load<Sprite>("Constellations/" + constellation.ImageSource);

        ScrollView.verticalNormalizedPosition = 1;

        MenuCanvas.enabled = true;
        ViewerCanvas.enabled = false;
        CameraScript.enabled = false;
    }

    public void CloseMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        MenuCanvas.enabled = false;
        CameraScript.enabled = true;
        ViewerCanvas.enabled = true;
    }
}
