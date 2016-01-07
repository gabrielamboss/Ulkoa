using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	public static int scoreCorrect, scoreWrong;
	public Text textCorrect, textWrong;


	// Use this for initialization
	void Start () {
		scoreCorrect = GlobalVariables.correctAnswerAmount;
		scoreWrong = GlobalVariables.wrongAnswerAmount;
		textCorrect.text = scoreCorrect.ToString();
		textWrong.text = scoreWrong.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
