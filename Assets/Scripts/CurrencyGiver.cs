using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CurrencyGiver : MonoBehaviour {
    public Button button;

    // Use this for initialization
    void Start() {
        if (GlobalVariables.WasNotDisplayed)
        {
            if (GlobalVariables.wrongAnswerAmount == 0)
            {
                ShowCurrencyGain();

                button.enabled = false;
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
        button.enabled = true;
        Destroy(gameObject);
    }

    private void GiveCurrency()
    {
        Player player = Player.getInstance();
        player.Currency++;
        player.SaveAsync();
    }
}
