using System;
using System.Collections.Generic;

[Serializable]
public class DeckListWrapper {

    public List<Deck> DeckList = new List<Deck>();

    public void Add(Deck deck)
    {
        DeckList.Add(deck);
    }
    public void Remove(Deck deck)
    {
        DeckList.Remove(deck);
    }
    public bool Contains(Deck deck)
    {
        return DeckList.Contains(deck);
    }

    public List<Deck> getList()
    {
        return DeckList;
    }
}
