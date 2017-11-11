using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationView : MonoBehaviour
{
    public Constellation Constellation { get; set; }

    // As component of the bound mesh
    //  also hold pointers to all the segment renderers, so they can be hidden/destroyed when the culture changes
}
