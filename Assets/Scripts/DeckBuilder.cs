using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DeckBuilder{

    private Deck deck = new Deck();    

    public DeckBuilder()
    {
        deck = Deck.createNewDeck();
    }

    public DeckBuilder(StoreDeck storeDeck)
    {
        Player player = Player.getInstance();

        deck = Deck.createNewDeck();
        deck.DeckName = storeDeck.DeckName;
        deck.UserId = player.UserId;
        deck.TimesPlayed = 0;
        deck.IsEditable = false;        

        List<StoreCard> storeCardList = storeDeck.getCards();
        foreach (StoreCard storeCard in storeCardList)
        {
            Card card = new Card();
            card.UserId = player.UserId;            
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
        card.UserId = Player.getInstance().UserId;
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
