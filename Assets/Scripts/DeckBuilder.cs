using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DeckBuilder{

    private Deck deck = new Deck();    

    public DeckBuilder()
    {
        deck = new Deck();
    }

    public DeckBuilder(StoreDeck storeDeck)
    {
        Player player = Player.getInstance();

        deck = new Deck();
        deck.DeckName = storeDeck.DeckName;
        deck.TimesPlayed = 0;
        deck.IsEditable = false;        

        List<Card> storeCardList = storeDeck.cardList;
        foreach (Card storeCard in storeCardList)
        {
            Card card = new Card();            
            card.LeitnerLevel = 1;
            card.PortugueseText = storeCard.PortugueseText;
            card.EnglishText = storeCard.EnglishText;
            deck.addCard(card);            
        }
    }

    public DeckBuilder setDeckName(string deckName)
    {
        deck.DeckName = deckName;
        return this;
    }

    public DeckBuilder setDeckEditable(bool isEditable)
    {
        deck.IsEditable = isEditable;
        return this;
    }

    public DeckBuilder addCard(string portText, string englishText)
    {
        Card card = new Card();        
        card.PortugueseText = portText;
        card.EnglishText = englishText;
        card.LeitnerLevel = 1;
        deck.addCard(card);
        return this;
    }

    public Deck getDeck()
    {
        return deck;
    }

}
