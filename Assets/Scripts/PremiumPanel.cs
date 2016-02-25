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


	//FAVOR USAR this.gameObject.GetComponent<Animator>().SetTrigger("movePremiumIn"); PARA FAZER O PAINEL IR DE FORA DA TELA PARA O MEIO
	//E this.gameObject.GetComponent<Animator>().SetTrigger("movePremiumOut"); PARA FAZÊ-LO IR DO MEIO DA TELA PARA FORA

    // Use this for initialization
    void Start ()
    {
        player = Player.getInstance();
        this.GetComponentInChildren<Text>().text = player.PremiumCredit.ToString();
	}
	
    public void showPremiumPanel(string nome)
    {
        Debug.Log(player.IsPremium);
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
			this.gameObject.GetComponent<Animator>().SetTrigger("movePremiumIn");
        }
    }

    public void closePremiumPanel()
    {
        if (player.PremiumCredit > 0)
        {
            mainMenu.enabled = true;
            matchHistory.enabled = true;
            evolution.enabled = true;
            savePlayer();
            DestroyImmediate(gameObject);
            SceneManager.LoadScene(LevelToGo);
        }

    }

    private void savePlayer()
    {
        Debug.Log("Saving player...");
        player.PremiumCredit--;
        StartCoroutine(new PlayerDao().savePlayer(player));
    }

    public void cancelPremiumPanel()
    {
        mainMenu.enabled = true;
        matchHistory.enabled = true;
        evolution.enabled = true;
        this.gameObject.GetComponent<Animator>().SetTrigger("movePremiumOut");
    }

	// Update is called once per frame
	void Update () {
        this.GetComponentInChildren<Text>().text = player.PremiumCredit.ToString();

    }
}
