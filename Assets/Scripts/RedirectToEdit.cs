using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class RedirectToEdit : MonoBehaviour
{
    public Button button;
    public InputField field;

    // Use this for initialization
    void Awake()
    {
        if (GlobalVariables.GetSelectedDeck().IsFirstTime)
        {
            ShowPopUp();

            button.enabled = false;
            field.enabled = false;
        }

    }

    private void ShowPopUp()
    {
        GlobalVariables.continueGame = false;
        GetComponentInParent<Animation>().Play();

    }

    // Update is called once per frame
    public void HidePopUp()
    {
        GlobalVariables.continueGame = true;
        button.enabled = true;
        field.enabled = true;
        GlobalVariables.GetSelectedDeck().IsFirstTime = false;
        //GlobalVariables.GetSelectedDeck().SaveAsync();
        Player.save();
        Redirect();
        Destroy(gameObject);

    }

    private void Redirect()
    {
        GlobalVariables.GoBackToGame = true;
        LevelManager levelManager = new LevelManager();
        levelManager.LoadLevel(SceneBook.DECK_CREATOR_NAME);
    }

    public void HidePopUpDecline()
    {
        GlobalVariables.continueGame = true;
        button.enabled = true;
        field.enabled = true;
        GlobalVariables.GetSelectedDeck().IsFirstTime = false;
        //GlobalVariables.GetSelectedDeck().SaveAsync();
        Player.save();
        Destroy(gameObject);
    }
}
