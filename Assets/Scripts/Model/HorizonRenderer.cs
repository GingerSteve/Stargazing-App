using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class HorizonRenderer : MonoBehaviour {


    public float width = 90;
    public float height = 90;
    public float x = 0;//start x pos
    public float y = 0;//start y pos
    public float theta = 180;
    public int resolution = 1000;//how many vertices there will be

    private float transSlider = 0.0f;//transparency value of the horizon

    private Vector3[] positions;
    public LineRenderer lr;

    void Start()
    {
        positions = CreateEllipse(width, height, x, y, theta, resolution);
        lr = GetComponent<LineRenderer>();

        //Set vertices and width 
        lr.SetVertexCount(resolution + 1);
        lr.SetWidth(0.25f, 0.25f);

        //Set Colour and Shader
        lr.material.color = Color.yellow;
        lr.material.shader = Shader.Find("Specular");

        //Use the line renderer to place connect each vertex
        for (int i = 0; i <= resolution; i++)
        {
            lr.SetPosition(i, positions[i]);
        }
    }

    void OnGUI()
    {

        if(Input.GetKey("left"))
        {
            transSlider -= 0.1f;
        }
        else if(Input.GetKey("right"))
        {
            transSlider += 0.1f;
        }

        transSlider = GUI.HorizontalSlider(new Rect(20, 135, 175, 30), transSlider, 0.0f, 10.0f);
        Color newColor = lr.material.color;
        newColor.a = transSlider;
        lr.material.color = newColor;
    }

    /*
     * Go around the circle creating points from 0 to resolution in the circle
     */
    Vector3[] CreateEllipse(float w, float h, float x, float y, float theta, int resolution)
    {
        positions = new Vector3[resolution + 1];
        Quaternion axis = Quaternion.AngleAxis(theta, Vector3.forward);//Quaternion rotates theta degrees 
        Vector3 center = new Vector3(x, y, 0.0f);

        for (int i = 0; i <= resolution; i++)
        {
            float angle = (float)i / (float)resolution * 2.0f * Mathf.PI;//calculate the angle in the circle for the pos i
            positions[i] = new Vector3(width * Mathf.Cos(angle), 0.0f,height * Mathf.Sin(angle));//create new Vector3 based on the angle
            positions[i] = axis * positions[i] + center;//calculate it's new position
        }

        return positions;
    }
}
