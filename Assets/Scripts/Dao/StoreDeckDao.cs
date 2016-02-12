using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class StoreDeckDao{

    StoreDeckListWrapper deckList = new StoreDeckListWrapper();
    List<StoreDeck> newDecks = new List<StoreDeck>();

    public IEnumerator MakeQueryGetDeckList()
    {
        deckList.addStoreDeck(mockQuerry());

        return null;
    }

    public List<StoreDeck> getQueryResultStoreDeckList()
    {
        return deckList.getList();
    }

    private List<StoreDeck> mockQuerry()
    {
        List<StoreDeck> list = new List<StoreDeck>();
        StoreDeck deck;
        for (int i = 1; i <= 10; i++)
        {
            deck = new StoreDeck();
            deck.DeckName = "SDeck" + i;
            deck.IsPremium = i > 7;
            deck.Price = i;
            for(int j = 1; j <= 5; j++)
            {
                Card card = new Card();
                card.PortugueseText = i + "Por" + j;
                card.EnglishText = i + "Eng" + j;
                deck.addCard(card);
            }
            list.Add(deck);
        }

        return list;
    }
}
