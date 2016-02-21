using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour {

    public GameObject deckStorePrefab;
    private GameObject collectionParent;
    private GameObject selectedDeckUI;
    public Text currency;
    public Sprite selectedDeckImage;
    public Sprite normalDeckImage;
    public GameObject cardContent;
    public GameObject cardTextModel;
    public GameObject painel;
    public Text username;
    public Text error;

    void Start () {

        collectionParent = GameObject.Find("Content");

        List<StoreDeck> deckList = Store.getDeckList();

        deckList.Sort(delegate(StoreDeck sd1, StoreDeck sd2)
        {
            if (!sd1.IsPremium && sd2.IsPremium)
                return -1;

            if (sd1.IsPremium && !sd2.IsPremium)
                return 1;

            return sd1.DeckName.CompareTo(sd2.DeckName);
        });
        
        foreach (StoreDeck deck in deckList)
        {
            GameObject newDeck = Instantiate(deckStorePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newDeck.transform.SetParent(collectionParent.transform, false);
            newDeck.GetComponent<StoreDeckUI>().SetStoreDeck(deck);
        }

        if (deckList.Count > 0)
        {
            selectedDeckUI = collectionParent.transform.GetChild(0).gameObject;
            OnDeckClick(collectionParent.transform.GetChild(0).gameObject);
        }

        Player player = Player.getInstance();
        username.text = player.Username;
              
        currency.text = Player.getInstance().Currency.ToString();        
    }

    public void OnDeckClick(GameObject deckUI)
    {
        selectedDeckUI.GetComponent<Image>().sprite = normalDeckImage;
        selectedDeckUI = deckUI;
        selectedDeckUI.GetComponent<Image>().sprite = selectedDeckImage;

        StoreDeck deck = deckUI.GetComponent<StoreDeckUI>().getStoreDeck();
        List<Card> storeCardList = deck.cardList;

        updateCardScroll(storeCardList);

        Debug.Log(deck.DeckName);
    }

    private void updateCardScroll(List<Card> storeCardList)
    {
        //Limpar card scroll;
        while (cardContent.transform.childCount > 0)
        {
            Transform child = cardContent.transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }

        storeCardList.Sort(delegate (Card sc1, Card sc2)
        {
            return sc1.PortugueseText.CompareTo(sc2.PortugueseText);
        });

        foreach (Card card in storeCardList)
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
        if (selectedDeckUI == null)
            return;

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

        Player player = Player.getInstance();
        player.addToStoreDeckNameList(deck.DeckName);
        player.Currency -= storeDeck.Price;
        currency.text = player.Currency.ToString();
        player.addDeck(deck);

        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.savePlayer(player);

        if (!playerDao.isSaveSuccessfull())
            error.text = "Erro ao salvar compra, porfavor verifique sua conexão";

        selectedDeckUI.transform.parent = null;
        Store.deletDeck(storeDeck);

        painel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

}
