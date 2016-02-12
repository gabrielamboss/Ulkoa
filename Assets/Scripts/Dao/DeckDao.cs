using UnityEngine;
using Parse;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//Provavelmente vou deletar essa classe
public class DeckDao {

    List<Deck> deckList = new List<Deck>();

    /*
    public IEnumerator MakeQueryGetDeckList(Player player)
    {
        ParseQuery<Deck> deckQuery = new ParseQuery<Deck>().WhereEqualTo("UserId", player.UserId);

        bool wait = true;
        deckQuery.FindAsync().ContinueWith(t => {
            deckList = t.Result.ToList<Deck>();
            wait = false;
        });
        while (wait)
        { yield return null; }

        List<Card> cardList = null;

        //ParseQuery<Card> cardQuery = new ParseQuery<Card>().WhereEqualTo("UserId", player.UserId);

        wait = true;
        /*
        cardQuery.FindAsync().ContinueWith(t => {
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

    public List<Deck> getQueryResultDeckList()
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
            //card.SaveAsync().ContinueWith(t => { count++; });
        }
        while (count < cardList.Count)
        { yield return null; }
    }

    public IEnumerator deleteDeck(Deck deck)
    {
        List<Card> cardList = deck.getCardList();

        int count = 0;
        foreach (Card card in cardList)
        {            
            //card.DeleteAsync().ContinueWith(t => { count++; });
        }
        while (count < cardList.Count)
        { yield return null; }

        bool wait = true;
        deck.DeleteAsync().ContinueWith(t => { wait = false; });
        while (wait)
        { yield return null; }        
    }
    */
}
