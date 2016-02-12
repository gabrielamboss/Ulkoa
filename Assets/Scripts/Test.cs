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
        //StartCoroutine(InitAndContinueWith(PreStore));
        aux();
    }

    [SerializeField]
    List<int> il = new List<int>() { 1, 2, 3 };
    private void aux()
    {
        List<Deck> deckList = new List<Deck>();
        
        List<string> sl = new List<string>() { "abc","def","ghi"};
        Player player = new Player();
        player.Username = "teste";
        DeckBuilder deckBuilder = new DeckBuilder()
                                .setDeckName("Default")
                                .addCard("DefPor1", "DefEng1")
                                .addCard("DefPor2", "DefEng2")
                                .addCard("DefPor3", "DefEng3")
                                .addCard("DefPor4", "DefEng4");

        deckList.Add(deckBuilder.getDeck());
        player.addDeck(deckBuilder.getDeck());
        player.addToStoreDeckNameList("atastsfsd");
        Debug.Log("Criando Json");        
        Debug.Log(JsonUtility.ToJson(deckList[0]));
        Debug.Log(JsonUtility.ToJson(deckList[0]));
        Debug.Log(JsonUtility.ToJson(player.DeckList));
        Debug.Log(JsonUtility.ToJson(player));
        Debug.Log(JsonUtility.ToJson(player.StoreDeckNameList));
        Debug.Log(JsonUtility.ToJson(il));
        Debug.Log(JsonUtility.ToJson(sl));
        Debug.Log("Json criado");
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
        while (!UlkoaInitializer.HasInitialized())
        {yield return null;}
        Method();
    }
    
}
