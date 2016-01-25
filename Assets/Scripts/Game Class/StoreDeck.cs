using Parse;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ParseClassName("StoreDeck")]
public class StoreDeck : ParseObject {

    private List<StoreCard> cardList = new List<StoreCard>();

    [ParseFieldName("DeckName")]
    public string DeckName
    {
        get { return GetProperty<string>("DeckName"); }
        set { SetProperty<string>(value, "DeckName"); }
    }

    [ParseFieldName("Price")]
    public int Price
    {
        get { return GetProperty<int>("Price"); }
        set { SetProperty<int>(value, "Price"); }
    }

    [ParseFieldName("IsPremium")]
    public bool IsPremium
    {
        get { return GetProperty<bool>("IsPremium"); }
        set { SetProperty<bool>(value, "IsPremium"); }
    }

    public void addCard(StoreCard card)
    {
        cardList.Add(card);
    }

    public List<StoreCard> getCards()
    {
        return cardList;
    }

}
