using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Deck
{   

    public string DeckName;
    public int TimesPlayed;
    public bool IsEditable;    
    public bool IsFirstTime;

    public List<Card> cardList = new List<Card>();

    public Deck()
    {
        DeckName = "";
        IsEditable = true;
        TimesPlayed = 0;
        IsFirstTime = true;
    }    

    public void addCard(Card card)
    {
        cardList.Add(card);
    }    

    public void CleanCardList()
    {
        cardList.Clear();
    }    

}