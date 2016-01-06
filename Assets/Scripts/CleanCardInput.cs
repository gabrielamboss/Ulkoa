using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CleanCardInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CleanInput() {
		InputField inputText = transform.GetComponentInChildren<InputField>();
		inputText.text = "";
	}

}
