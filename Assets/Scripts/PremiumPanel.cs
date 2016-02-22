using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class PremiumPanel : MonoBehaviour {
    private Player player;
    private string LevelToGo;
    public Button mainMenu;
    public Button matchHistory;
    public Button evolution;
    public GameObject PanelPrefab;

    // Use this for initialization
    void Start ()
    {
        player = Player.getInstance();
        this.GetComponentInChildren<Text>().text = player.PremiumCredit.ToString();
	}
	
    public void showPremiumPanel(string nome)
    {
        if (player.IsPremium || GlobalVariables.PremiumWasDisplayed) SceneManager.LoadScene(nome);
        else if (player.PremiumCredit == 5)
        {
            savePlayer();
            GlobalVariables.PremiumWasDisplayed = true;
            SceneManager.LoadScene(nome);
        }
        else
        {
            mainMenu.enabled = false;
            matchHistory.enabled = false;
            evolution.enabled = false;
            GlobalVariables.PremiumWasDisplayed = true;
            LevelToGo = nome;
            GetComponentInParent<Animation>().Play();
        }
    }

    public void closePremiumPanel()
    {
        mainMenu.enabled = true;
        matchHistory.enabled = true;
        evolution.enabled = true;
        savePlayer();
        DestroyImmediate(gameObject);
        SceneManager.LoadScene(LevelToGo);
    }

    private void savePlayer()
    {
        player.PremiumCredit--;
        StartCoroutine(new PlayerDao().savePlayer(player));
    }

    public void cancelPremiumPanel()
    {
        mainMenu.enabled = true;
        matchHistory.enabled = true;
        evolution.enabled = true;
        DestroyImmediate(gameObject);
        GameObject panel = Instantiate(PanelPrefab, new Vector3(500, 500, 0), Quaternion.identity) as GameObject;
    }

	// Update is called once per frame
	void Update () {
        this.GetComponentInChildren<Text>().text = player.PremiumCredit.ToString();

    }
}
