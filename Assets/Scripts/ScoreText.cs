using Parse;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	public static int scoreCorrect, scoreWrong;
	private float percentScore;
	public Text textCorrect, textWrong;

    void Awake()
    {
        ParseObject.RegisterSubclass<Match>();
    }

	// Use this for initialization
	void Start () {
		scoreCorrect = GlobalVariables.correctAnswerAmount;
		scoreWrong = GlobalVariables.wrongAnswerAmount;
		percentScore = ((float)scoreCorrect/((float)scoreCorrect+(float)scoreWrong));
		textCorrect.text = scoreCorrect.ToString();
		textWrong.text = scoreWrong.ToString();

        //Saving match
        Match match = new Match();
        match.CorrectPoints = scoreCorrect;
        match.WrongPoints = scoreWrong;
        match.SaveAsync();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
