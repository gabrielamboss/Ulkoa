using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FlipCorrectAnswer : MonoBehaviour {
    private String correctAnswer;
	// Use this for initialization
	void Start () {
        correctAnswer = GameManager.GetCorrectAnswer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void flipCorrectAnswer()
    {
        gameObject.GetComponentInChildren<Text>().text = correctAnswer;
        GetComponent<AudioSource>().Play();
    }
}
