using Parse;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckCreatorModel : MonoBehaviour {

    private DeckCreatorView view = null;

    private Deck deck;
    private List<Card> cardList;
    private List<Card> cardsToDelet;
    private Card selectedCard;
    private Card newCard;

    // Use this for initialization
    void Awake()
    {
        GameObject deckCreator = GameObject.Find("Deck Creator");
        view = deckCreator.GetComponent<DeckCreatorView>();
    }

    void Start () {

        deck = GlobalVariables.GetSelectedDeck();
        cardList = deck.getCardList();
        cardsToDelet = new List<Card>();

        newCard = new Card();
        newCard.PortugueseText = "Nova Carta";
        newCard.EnglishText = "New Card";
        selectedCard = newCard;

        view.UpdateScreen();
	}
	
	
    public List<Card> GetCardList()
    {
        return cardList;
    }

    public Card GetNewCard()
    {
        return newCard;
    }

    public Card GetSelectedCard()
    {
        return selectedCard;
    }

    public void SetSelectedCard(Card card)
    {
        selectedCard = card;
    }

    public void CreateNewCard()
    {
        cardList.Add(newCard);
        newCard = new Card();
        newCard.PortugueseText = "Nova Carta";
        newCard.EnglishText = "New Card";
        newCard.LeitnerLevel = 1;
    }

    public void DeletSelectedCard()
    {
        cardList.Remove(selectedCard);
        cardsToDelet.Add(selectedCard);
        selectedCard = newCard;
    }

    public void Save()
    {
        //Save deck first
        //Its just a test

        foreach (Card card in cardList)
        {
            card.UserId = ParseUser.CurrentUser.ObjectId;
            card.DeckId = deck.ObjectId;
            card.SaveAsync();
        }
        foreach (Card card in cardsToDelet)
        {
            card.DeleteAsync();
        }
    }
}
