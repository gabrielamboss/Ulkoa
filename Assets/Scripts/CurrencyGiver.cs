using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CurrencyGiver : MonoBehaviour {
    public Button mainMenu;
    public Button matchHistory;
    public Button evolution;

    // Use this for initialization
    void Start() {
        if (GlobalVariables.WasNotDisplayed)
        {
            if (GlobalVariables.wrongAnswerAmount == 0)
            {
                ShowCurrencyGain();
                mainMenu.enabled = false;
                matchHistory.enabled = false;
                evolution.enabled = false;

            }
        }
	
	}

    private void ShowCurrencyGain()
    {
        GlobalVariables.WasNotDisplayed = false;
        GetComponentInParent<Animation>().Play();
    }

    // Update is called once per frame
    public void HidePopUp()
    {
        GiveCurrency();
        mainMenu.enabled = true;
        matchHistory.enabled = true;
        evolution.enabled = true;
        Destroy(gameObject);
    }

    private void GiveCurrency()
    {
        Player player = Player.getInstance();
        player.Currency++;
        new PlayerDao().savePlayer(player);        
    }
}
