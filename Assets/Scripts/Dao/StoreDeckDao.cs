using UnityEngine;
using Parse;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class StoreDeckDao : MonoBehaviour {

    List<StoreDeck> deckList;

    public IEnumerator MakeQueryGetDeckList()
    {
        ParseQuery<StoreDeck> deckQuery = new ParseQuery<StoreDeck>();

        bool wait = true;
        deckQuery.FindAsync().ContinueWith(t => {
            deckList = t.Result.ToList<StoreDeck>();
            wait = false;
        });
        while (wait)
        { yield return null; }

        List<StoreCard> cardList = null;

        ParseQuery<StoreCard> cardQuery = new ParseQuery<StoreCard>();

        wait = true;
        cardQuery.FindAsync().ContinueWith(t => {
            cardList = t.Result.ToList<StoreCard>();
            wait = false;
        });
        while (wait)
        { yield return null; }

        Dictionary<string, StoreDeck> dict = new Dictionary<string, StoreDeck>();
        foreach (StoreDeck deck in deckList)
        {
            dict[deck.ObjectId] = deck;
        }

        foreach (StoreCard card in cardList)
        {
            dict[card.StoreDeckId].addCard(card);
        }
    }

    public List<StoreDeck> getQueryResultStoreDeckList()
    {
        return deckList;
    }
}
