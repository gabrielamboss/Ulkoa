using System;
using System.Collections.Generic;

[Serializable]
public class StoreDeck{        
    public string DeckName;
    public int Price;
    public bool IsPremium;
    public List<Card> cardList = new List<Card>();

    public void addCard(Card card)
    {
        cardList.Add(card);        
    }

}
