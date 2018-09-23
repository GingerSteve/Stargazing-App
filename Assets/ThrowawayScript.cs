using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowawayScript : MonoBehaviour {


	public Dropdown dropdown1;

	List<string> cultures = new List<string>(){"Ancient", "Ojibwe"};

	// Use this for initialization
	void Start () {

		dropdown1.AddOptions (cultures);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
