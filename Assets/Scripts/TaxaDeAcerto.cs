using UnityEngine;
using System.Collections;
using ProgressBar;

public class TaxaDeAcerto : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInParent<ProgressRadialBehaviour>().SetFillerSizeAsPercentage(GlobalVariables.correctAnswerAmount * 100 / (GlobalVariables.correctAnswerAmount + GlobalVariables.wrongAnswerAmount));


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
