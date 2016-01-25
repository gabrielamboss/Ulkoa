using Parse;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Test : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        InitAndContinueWith(PreStore);
    }

    private IEnumerator aux()
    {
        StoreDeck deck = new StoreDeck();
        deck.DeckName = "Cartas1";
        deck.Price = 100;
        deck.IsPremium = false;

        bool wait = true;
        deck.SaveAsync().ContinueWith(t =>
        {
            wait = false;
        });
        while (wait)
        {
            yield return null;
        }

        StoreCard card1 = new StoreCard();
        card1.StoreDeckId = deck.ObjectId;
        card1.PortugueseText = "Port1";
        card1.EnglishText = "Engl1";
        card1.SaveAsync().ContinueWith(t => { Debug.Log("Finish"); });

        StoreCard card2 = new StoreCard();
        card2.StoreDeckId = deck.ObjectId;
        card2.PortugueseText = "Port1";
        card2.EnglishText = "Engl1";
        card2.SaveAsync().ContinueWith(t => { Debug.Log("Finish"); });
    }

    public void GoGoGo()
    {
        new LevelManager().LoadLevel(SceneBook.STORE_NAME);
    }

    private void PreStore()
    {
        Debug.Log("PreStore has finished");
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
        //Player2.Init();
        Debug.Log("Hisssss");
        StartCoroutine(UlkoaInitializer.InitializeGame());
        StartCoroutine(ContinueWith(Method));
    }

    private IEnumerator ContinueWith(Action Method)
    {
        //while (!Player2.IsInitialized())
        while (!UlkoaInitializer.hasInitialied())
        {
            yield return null;
        }
        Method();
    }
}
