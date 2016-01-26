using Parse;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckCreatorModel : MonoBehaviour {
    public GameObject cardUI;

    private DeckCreatorView view;
    private InputField deckName;
    private InputField portugueseText;
    private InputField englishText;

    private Deck deck;
    //private List<Card> cardList;
    private List<GameObject> cardUIList;
    private List<Card> cardsToDelete;
    private GameObject selectedCard;
    private bool waitSaveDeck; 

    // Use this for initialization
    void Awake()
    {        
        GameObject deckCreator = GameObject.Find("Deck Creator");
        view = deckCreator.GetComponent<DeckCreatorView>();

        GameObject deckNameGO = GameObject.Find("Deck Name");
        deckName = deckNameGO.GetComponent<InputField>();

        GameObject portObj = GameObject.Find("Portuguese Field");
        portugueseText = portObj.GetComponent<InputField>();

        GameObject englObj = GameObject.Find("English Field");
        englishText = englObj.GetComponent<InputField>();
    }

    void Start () {

        deck = GlobalVariables.GetSelectedDeck();
        cardUIList = new List<GameObject>();
        List<Card> cardList = deck.getCardList();
        foreach (Card card in cardList)
        {
            cardUIList.Add(CreateUICard(card));
        }
        cardsToDelete = new List<Card>();

        if(cardUIList.Count == 0)        
            CreateNewCard();
        
        selectedCard = cardUIList[0];

        view.SetDeckNameScreen();
        view.UpdateScreen();
	}
	
	
    public List<GameObject> GetCardList()
    {
        return cardUIList;
    }
    
    public string GetDeckName()
    {
        return deck.DeckName;
    }

    public string GetPortugueseText()
    {
        return portugueseText.text;
    }

    public string GetEnglishText()
    {
        return englishText.text;
    }

    public GameObject GetSelectedCard()
    {
        return selectedCard;
    }

    public void SetSelectedCard(Card card)
    {
        foreach (GameObject cardUI in cardUIList)
        {
            Card thisCard = cardUI.GetComponent<CardHolder>().GetCard();
            if (thisCard.Equals(card))
            {
                selectedCard = cardUI;
            }
        }        
    }

    public void CreateNewCard()
    {
        Card newCard = new Card();
        newCard.PortugueseText = "Nova Carta";
        newCard.EnglishText = "New Card";
        newCard.LeitnerLevel = 1;
        cardUIList.Add(CreateUICard(newCard));                
    }

    public void DeleteSelectedCard()
    {
        cardUIList.Remove(selectedCard);
        cardsToDelete.Add(selectedCard.GetComponent<CardHolder>().GetCard());

        if (cardUIList.Count == 0)        
            CreateNewCard();
        
        selectedCard = cardUIList[0];        
    }

    private GameObject CreateUICard(Card card) {
        GameObject cardObject = Instantiate(cardUI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        cardObject.GetComponent<CardHolder>().SetCard(card);
        cardObject.GetComponentInChildren<Text>().text = card.PortugueseText;
        return cardObject;
    }

    public void Save()
    {
        //Save deck first
        //Its just a test
        waitSaveDeck = true;
        deck.DeckName = deckName.text;
        deck.UserId = ParseUser.CurrentUser.ObjectId;
        deck.IsEditable = true;
        deck.TimesPlayed = 0;
        deck.SaveAsync().ContinueWith(t=>
        {
            waitSaveDeck = false;
        });

        StartCoroutine(SaveAndDeleteCards());        
    }

    private IEnumerator SaveAndDeleteCards()
    {
        while (waitSaveDeck)
        {
            yield return null;
        }

        deck.CleanCardList();

        foreach (GameObject cardGO in cardUIList)
        {
            Card card = cardGO.GetComponent<CardHolder>().GetCard();
            card.UserId = ParseUser.CurrentUser.ObjectId;
            card.DeckId = deck.ObjectId;
            deck.addCard(card);
            card.SaveAsync();
        }

        foreach (Card card in cardsToDelete)
        {
            card.DeleteAsync();
        }

        Player player = Player.getInstance();

        if(!player.hasDeck(deck))
            player.addDeck(deck);

        cardsToDelete.Clear();
    }
}
