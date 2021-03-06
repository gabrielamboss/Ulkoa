﻿using System.Collections.Generic;

public class Store{

    private static List<StoreDeck> deckList = new List<StoreDeck>();

    public static List<StoreDeck> getDeckList()
    {
        return deckList;
    }

    public static void addDeck(StoreDeck deck)
    {
        deckList.Add(deck);
    }

    public static List<StoreDeck> getDeck()
    {
        return deckList;
    }
    
    public static void deletDeck(StoreDeck deck)
    {
        deckList.Remove(deck);
    }

    public static void clean()
    {
        deckList.Clear();
    }

}
