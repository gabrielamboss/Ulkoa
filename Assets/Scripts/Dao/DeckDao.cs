using UnityEngine;
using Parse;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class DeckDao {

    List<Deck> deckList;

    public IEnumerator MakeQuerryGetDeckList(Player player)
    {
        ParseQuery<Deck> deckQuerry = new ParseQuery<Deck>().WhereEqualTo("UserId", player.UserId);

        bool wait = true;
        deckQuerry.FindAsync().ContinueWith(t => {
            deckList = t.Result.ToList<Deck>();
            wait = false;
        });
        while (wait)
        { yield return null; }

        List<Card> cardList = null;

        ParseQuery<Card> cardQuerry = new ParseQuery<Card>().WhereEqualTo("UserId", player.UserId);

        wait = true;
        cardQuerry.FindAsync().ContinueWith(t => {
            cardList = t.Result.ToList<Card>();
            wait = false;
        });
        while (wait)
        { yield return null; }

        Dictionary<string, Deck> dict = new Dictionary<string, Deck>();
        foreach (Deck deck in deckList)
        {
            dict[deck.ObjectId] = deck;            
        }

        foreach (Card card in cardList)
        {
            dict[card.DeckId].addCard(card);
        }
    }

    public List<Deck> getQuerryResultDeckList()
    {
        return deckList;
    }

    public IEnumerator saveDeck(Deck deck)
    {
        bool wait = true;
        deck.SaveAsync().ContinueWith(t => { wait = false; });
        while (wait)
        { yield return null; }

        List<Card> cardList = deck.getCardList();

        int count = 0;
        foreach (Card card in cardList)
        {
            card.UserId = Player.getInstance().UserId;
            card.DeckId = deck.ObjectId;
            card.SaveAsync().ContinueWith(t => { count++; });
        }
        while (count < cardList.Count)
        { yield return null; }
    }

    public IEnumerator deleteDeck(Deck deck)
    {
        bool wait = true;
        deck.DeleteAsync().ContinueWith(t => { wait = false; });
        while (wait)
        { yield return null; }

        List<Card> cardList = deck.getCardList();

        int count = 0;
        foreach (Card card in cardList)
        {
            card.UserId = Player.getInstance().UserId;
            card.DeckId = deck.ObjectId;
            card.DeleteAsync().ContinueWith(t => { count++; });
        }
        while (count < cardList.Count)
        { yield return null; }
    }
}
