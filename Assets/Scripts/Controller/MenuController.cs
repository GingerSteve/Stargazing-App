using System;
using System.Collections.Generic;
using UnityEngine;
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

    void Update()
    {

    }

    public void OpenMenu(GameObject constellation)
    {
        Screen.orientation = (ScreenOrientation)CameraScript.GetOrientation();
        Screen.orientation = ScreenOrientation.AutoRotation;

        //NameText.text = item.Name;
        //DescText.text = item.Desc;
        //ConstImage.sprite = Resources.Load<Sprite>(item.Image);

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
