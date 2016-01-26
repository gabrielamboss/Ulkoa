using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CurrencyGiver : MonoBehaviour {
    public Button button;

    // Use this for initialization
    void Start() {
        if (GlobalVariables.wrongAnswerAmount == 0)
        {
            ShowCurrencyGain();
            button.enabled = false;        }
	
	}

    private void ShowCurrencyGain()
    {
        gameObject.transform.position = new Vector3(400, 330, 0);
    }

    // Update is called once per frame
    public void HidePopUp()
    {
        button.enabled = true;
        Destroy(gameObject);
    }
}
