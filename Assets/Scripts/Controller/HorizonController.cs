using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class HorizonController : MonoBehaviour {


    public float Width = 90;
    public float Height = 90;
    public float XPos = 0;//start x pos
    public float YPos = 0;//start y pos
    public float Theta = 180;
    public int Resolution = 50;//how many vertices there will be

    private float lineWidth = 0.25f;

    private Vector3[] positions;
    private LineRenderer lineRen;


    void Start()
    {
        positions = CreateEllipse(Width, Height, XPos, YPos, Theta, Resolution);
        lineRen = GetComponent<LineRenderer>();

        //Set vertices and width 
        lineRen.positionCount = Resolution + 1;
        lineRen.startWidth = lineWidth;
        lineRen.endWidth = lineWidth;

        //Use the line renderer to place connect each vertex
        for (int i = 0; i <= Resolution; i++)
        {
            lineRen.SetPosition(i, positions[i]);
        }
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
            positions[i] = new Vector3(w * Mathf.Cos(angle), 0.0f, h * Mathf.Sin(angle));//create new Vector3 based on the angle
            positions[i] = axis * positions[i] + center;//calculate it's new position
        }

        return positions;
    }
}
