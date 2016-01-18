using Parse;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Test : MonoBehaviour {
       
    // Use this for initialization
    void Start () {
        InitAndContinueWith(PreMainMenu);        
    }

    public void GoGoGo()
    {
        new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);
    }

    private void PreDeckCreator()
    {
        List<Deck> deckList = Player2.GetDeckList();
        foreach (Deck deck in deckList)
        {
            if (deck.DeckName.Equals("DeckTeste"))
            {
                GlobalVariables.SetSelectedDeck(deck);
            }
        }

        Debug.Log("PreDeckCreator3 has finished");
    }

    private void PreMainMenu()
    {
        Debug.Log("PreMainMenu has finished");
    }

    

    private void InitAndContinueWith(Action Method)
    {
        Player2.Init();
        StartCoroutine(ContinueWith(Method));
    }

    private IEnumerator ContinueWith(Action Method)
    {
        while (!Player2.IsInitialized())
        {
            yield return null;
        }
        Method();
    }
}
