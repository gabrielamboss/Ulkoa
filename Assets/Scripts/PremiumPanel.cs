using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class PremiumPanel : MonoBehaviour {
    private Player player;
    private string LevelToGo;
	// Use this for initialization
	void Start ()
    {
        player = Player.getInstance();
        this.GetComponentInChildren<Text>().text = player.PremiumCredit.ToString();
	}
	
    public void showPremiumPanel(string nome)
    {
        if (player.IsPremium || GlobalVariables.PremiumWasDisplayed) SceneManager.LoadScene(nome);
        else
        {
            GlobalVariables.PremiumWasDisplayed = true;
            LevelToGo = nome;
            GetComponentInParent<Animation>().Play();
        }
    }

    public void closePremiumPanel()
    {
        player.PremiumCredit--;
        StartCoroutine(new PlayerDao().savePlayer(player));
        DestroyImmediate(gameObject);
        SceneManager.LoadScene(LevelToGo);
    }

	// Update is called once per frame
	void Update () {
        this.GetComponentInChildren<Text>().text = player.PremiumCredit.ToString();

    }
}
