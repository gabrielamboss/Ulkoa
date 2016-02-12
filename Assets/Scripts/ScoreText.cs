using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	public static int scoreCorrect, scoreWrong;
	private float percentScore;
	public Text textCorrect, textWrong;    

	// Use this for initialization
	void Start () {
		scoreCorrect = GlobalVariables.correctAnswerAmount;
		scoreWrong = GlobalVariables.wrongAnswerAmount;
		percentScore = ((float)scoreCorrect/((float)scoreCorrect+(float)scoreWrong));
		textCorrect.text = scoreCorrect.ToString();
		textWrong.text = scoreWrong.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
