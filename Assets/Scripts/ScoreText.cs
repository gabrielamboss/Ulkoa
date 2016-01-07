using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	public static int scoreCorrect, scoreWrong;
	private float percentScore;
	public Text textCorrect, textWrong, textPercent;


	// Use this for initialization
	void Start () {
		scoreCorrect = GlobalVariables.correctAnswerAmount;
		scoreWrong = GlobalVariables.wrongAnswerAmount;
		percentScore = ((float)scoreCorrect/((float)scoreCorrect+(float)scoreWrong));
		textCorrect.text = scoreCorrect.ToString();
		textWrong.text = scoreWrong.ToString();
		textPercent.text = (percentScore*100).ToString("n2")+"%";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
