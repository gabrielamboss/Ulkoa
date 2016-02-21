using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class Test : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        //InitAndContinueWith(PreStore);
        //StartCoroutine(InitAndContinueWith(PreStore));
        StartCoroutine(matchDaoTeste());
    }
    
    private void storeDeckMaker()
    {        
        StoreDeck deck;
        for (int i = 1; i <= 10; i++)
        {
            deck = new StoreDeck();
            deck.DeckName = "SDeck" + i;
            deck.IsPremium = i > 7;
            deck.Price = i;
            for (int j = 1; j <= 5; j++)
            {
                Card card = new Card();
                card.PortugueseText = i + "Por" + j;
                card.EnglishText = i + "Eng" + j;
                deck.addCard(card);
            }

            Debug.Log("DeckName: " + deck.DeckName);
            Debug.Log(JsonUtility.ToJson(deck));
        }
    }

    private IEnumerator storeDeckDaoTeste()
    {

        yield return makeConnection();

        StoreDeckDao stDao = new StoreDeckDao();
        //yield return stDao.MakeQueryGetDeckList();

        Debug.Log("Escrevendo resultado");
        List<StoreDeck> l = stDao.getQueryResultStoreDeckList();
        foreach(StoreDeck deck in l)
        {
            Debug.Log(deck.DeckName);
        }

    }    

    private IEnumerator matchDaoTeste()
    {
        yield return makeConnection();

        MatchDao matchDao = new MatchDao();
        //yield return matchDao.MakeQueryGetMatchList();

        foreach(Match match in matchDao.getMatchList())
        {
            Debug.Log(match.DeckName + ": " +  match.MatchNumber);
        }
        
    }

    private IEnumerator makeConnection()
    {
        PlayFabSettings.TitleId = "2071";
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            TitleId = "2071",
            Username = "teste6",
            Password = "teste6"
        };

        bool wait = true;
        Debug.Log("Tentando logar");
        PlayFabClientAPI.LoginWithPlayFab(request,
            (result) =>
            {
                wait = false;
                if (result.NewlyCreated)
                {
                    Debug.Log("Merda criamos um novo usuario");
                }
                else
                {
                    Debug.Log("Login com sucesso");
                }
            },
            (error) =>
            {
                wait = false;
                Debug.Log("Error logging in player");
                Debug.Log(error.ErrorMessage);
            });
        while (wait) { yield return null; }
    }

    public void GoGoGo()
    {
        new LevelManager().LoadLevel(SceneBook.STORE_NAME);
    }

    private void PreStore()
    {
        //Player player = Player.getInstance();
        //StoreDeck deck = new StoreDeck();
        //deck.DeckName = "galinha";
        //player.AddToList("StoreDeckNameList", deck.DeckName);
        //player.SaveAsync().ContinueWith(t=> { Debug.Log("FIMFIMFIM"); });
        Debug.Log("FIMFIMFIM");
    }    

    private void PreMainMenu()
    {
        Debug.Log("PreMainMenu has finished");
    }

    private IEnumerator InitAndContinueWith(Action Method)
    {        
        yield return UlkoaInitializer.InitializeGame();
        //while (!UlkoaInitializer.HasInitialized())
        {yield return null;}
        Method();
    }
    
}
