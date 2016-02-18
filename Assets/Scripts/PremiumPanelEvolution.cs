using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PremiumPanelEvolution : MonoBehaviour {
    private Player player;
    private 
	// Use this for initialization
	void Start () {
        player = Player.getInstance();
        if (player.IsPremium || GlobalVariables.WasDisplayedEvolution) DestroyImmediate(gameObject);
        this.GetComponentInChildren<Text>().text = player.PremiumCredit.ToString();

	}
	
    public void closePremiumPanel()
    {
        GlobalVariables.WasDisplayedEvolution = true;
        player.PremiumCredit--;
        DestroyImmediate(gameObject);
    }

	// Update is called once per frame
	void Update () {
        this.GetComponentInChildren<Text>().text = player.PremiumCredit.ToString();

    }
}
