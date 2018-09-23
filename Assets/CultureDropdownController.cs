using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows and populates culure menu dropdown with desired cultures and contains methods to show or hide dropdown
/// </summary>
public class CultureDropdownController : MonoBehaviour {

	public Dropdown dropdown;

	List<string> cultures  = new List<string>() {"Ancient", "Ojibwe"};

	void Start () {

		dropdown.AddOptions (cultures);
		dropdown.gameObject.SetActive (false);

	}
		
	/// <summary>
	/// Toggles that active state of the dropdown. 
	/// Notice the DestroyObject statement is a workaround to a bug in Unity's dropdown 
	/// </summary>
	public void ToggleActive(){
		
		if (gameObject.activeSelf) {
			if (gameObject.GetComponentInChildren<Canvas> () != null) {
				DestroyObject (dropdown.GetComponentInChildren<Canvas> ().gameObject);
				gameObject.SetActive (false);
			}
		} else {
			gameObject.SetActive (true);
		}
	}
		
}
