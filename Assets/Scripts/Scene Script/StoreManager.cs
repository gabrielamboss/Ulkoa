using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour {

    public GameObject deckStorePrefab;
    private GameObject collectionParent;
    private GameObject selectedDeckUI;
    private Text currency;
    public GameObject cardContent;
    public GameObject cardTextModel;
    public GameObject painel;

    void Start () {

        collectionParent = GameObject.Find("Content");

        List<StoreDeck> deckList = Store.getDeckList();

        foreach (StoreDeck deck in deckList)
        {
            GameObject newDeck = Instantiate(deckStorePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newDeck.transform.SetParent(collectionParent.transform, false);
            newDeck.GetComponent<StoreDeckUI>().SetStoreDeck(deck);
        }

        currency = GameObject.Find("Currency").GetComponent<Text>();
        currency.text = Player.getInstance().Currency.ToString();
    }

    public void OnDeckClick(GameObject deckUI)
    {
        selectedDeckUI = deckUI;

        StoreDeck deck = deckUI.GetComponent<StoreDeckUI>().getStoreDeck();
        List<StoreCard> storeCardList = deck.getCards();

        updateCardScroll(storeCardList);

        Debug.Log(deck.DeckName);
    }

    private void updateCardScroll(List<StoreCard> storeCardList)
    {
        //Limpar card scroll;
        while (cardContent.transform.childCount > 0)
        {
            Transform child = cardContent.transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }

        foreach (StoreCard card in storeCardList)
        {
            GameObject newCard = Instantiate(cardTextModel, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newCard.transform.SetParent(cardContent.transform, false);
            newCard.GetComponent<Text>().text = card.PortugueseText;

            newCard = Instantiate(cardTextModel, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newCard.transform.SetParent(cardContent.transform, false);
            newCard.GetComponent<Text>().text = card.EnglishText;

            newCard.transform.localScale = new Vector3(1, 1, 1);
        }
    }
	
    public void Exit()
    {
        new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);
    }

    public void Buy()
    {
        StoreDeck storeDeck = selectedDeckUI.GetComponent<StoreDeckUI>().getStoreDeck();

        if (storeDeck.IsPremium)
        {
            Debug.Log("Usuarios nao premium nao podem ter esse deck");
            return;
        }

        if (Player.getInstance().Currency < storeDeck.Price)
        {
            Debug.Log("Voce nao tem moedas suficientes");
            return;
        }
        
        StartCoroutine(saveDeck(storeDeck));
    }

    private IEnumerator saveDeck(StoreDeck storeDeck)
    {
        painel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();

        DeckBuilder deckBuilder = new DeckBuilder(storeDeck);
        Deck deck = deckBuilder.getDeck();

        DeckDao deckDao = new DeckDao();
        yield return deckDao.saveDeck(deck);

        Player player = Player.getInstance();
        player.AddToList("StoreDeckNameList", deck.DeckName);
        player.Currency -= storeDeck.Price;
        currency.text = player.Currency.ToString();
        player.addDeck(deck);

        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.savePlayer(player);

        selectedDeckUI.transform.parent = null;
        Store.deletDeck(storeDeck);

        painel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

}
