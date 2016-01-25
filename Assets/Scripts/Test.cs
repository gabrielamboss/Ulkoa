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
        //InitAndContinueWith(PreStore);
        StartCoroutine(aux());
    }

    private IEnumerator aux()
    {
        StoreDeck deck;
        StoreCard card;
        bool wait;

        for (int i = 1; i <= 10; i++)
        {
            deck = new StoreDeck();
            deck.DeckName = "SD"+i;
            deck.Price = i;
            deck.IsPremium = i >= 8;

            wait = true;
            deck.SaveAsync().ContinueWith(t =>{wait = false;});
            while (wait)
            {yield return null;}
            Debug.Log("Deck SD" + i + " foi salvo");

            for(int j= 1; j <= 5; j++)
            {
                card = new StoreCard();
                card.StoreDeckId = deck.ObjectId;
                card.PortugueseText = "SD" + i + "P" + j;
                card.EnglishText = "SD" + i + "E" + j;
                wait = true;
                card.SaveAsync().ContinueWith(t => { wait = false; });
                while (wait)
                { yield return null; }
                Debug.Log("Carta " + j + " salva");
            }

            Debug.Log("Fim da criacao do deck " + i);
        }
        
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
