using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour {

    public GameObject deckStorePrefab;
    private GameObject collectionParent;
    private GameObject selectedDeckUI;
    
   
    void Start () {

        collectionParent = GameObject.Find("Content");

        List<StoreDeck> deckList = Store.getDeckList();

        foreach (StoreDeck deck in deckList)
        {
            GameObject newDeck = Instantiate(deckStorePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newDeck.transform.SetParent(collectionParent.transform, false);
            newDeck.GetComponent<StoreDeckUI>().SetStoreDeck(deck);
        }

    }

    public void OnDeckClick(GameObject deckUI)
    {
        selectedDeckUI = deckUI;
        StoreDeck deck = deckUI.GetComponent<StoreDeckUI>().getStoreDeck();
        Debug.Log(deck.DeckName);
    }
	
    public void Exit()
    {
        new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);
    }

    public void Buy()
    {
        //Falta a logica de compra !!!!
        selectedDeckUI.transform.parent = null;
        StoreDeck storeDeck = selectedDeckUI.GetComponent<StoreDeckUI>().getStoreDeck();
        StartCoroutine(saveDeck(storeDeck));
    }

    private IEnumerator saveDeck(StoreDeck storeDeck)
    {
        
        Deck deck = new Deck();
        deck.DeckName = storeDeck.DeckName;
        deck.UserId = Player.getInstance().UserId;
        deck.TimesPlayed = 0;
        deck.IsEditable = false;

        bool wait = true;
        deck.SaveAsync().ContinueWith(t=>{ wait = false; });
        while (wait)
        {yield return null;}

        List<StoreCard> storeCardList = storeDeck.getCards();
        foreach (StoreCard storeCard in storeCardList)
        {
            Card card = new Card();
            card.UserId = Player.getInstance().UserId;
            card.DeckId = deck.ObjectId;
            card.LeitnerLevel = 1;
            card.PortugueseText = storeCard.PortugueseText;
            card.EnglishText = storeCard.EnglishText;
            deck.addCard(card);
            card.SaveAsync();
        }

        Player player = Player.getInstance();
        player.AddToList("StoreDeckNameList", deck.DeckName);                
        player.addDeck(deck);
        wait = true;
        player.SaveAsync().ContinueWith(t => { wait = false; });
        while (wait)
        { yield return null; }
        Store.deletDeck(storeDeck);
    }

}
