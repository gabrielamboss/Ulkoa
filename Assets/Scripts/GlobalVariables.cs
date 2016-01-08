using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

	public static int correctAnswerAmount, wrongAnswerAmount;
	public static bool facebookLogin = false;
	public static bool normalLogin = false;

	public static void ResetCorrect(){
		correctAnswerAmount = 0;
	}

	public static void ResetWrong(){
		wrongAnswerAmount = 0;
	}

	public static void Reset(){
		ResetCorrect();
		ResetWrong();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
