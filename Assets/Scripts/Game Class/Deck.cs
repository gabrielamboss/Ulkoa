using UnityEngine;
using Parse;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ParseClassName("Deck")]
public class Deck : ParseObject
{

    private List<Card> cardList = new List<Card>();

    [ParseFieldName("DeckName")]
    public string DeckName
    {
        get { return GetProperty<string>("DeckName"); }
        set { SetProperty<string>(value, "DeckName"); }
    }

    [ParseFieldName("UserId")]
    public string UserId
    {
        get { return GetProperty<string>("UserId"); }
        set { SetProperty<string>(value, "UserId"); }
    }

    [ParseFieldName("IsEditable")]
    public bool IsEditable
    {
        get { return GetProperty<bool>("IsEditable"); }
        set { SetProperty<bool>(value, "IsEditable"); }
    }

    [ParseFieldName("TimesPlayed")]
    public int TimesPlayed
    {
        get { return GetProperty<int>("TimesPlayed"); }
        set { SetProperty<int>(value, "TimesPlayed"); }
    }

    [ParseFieldName("IsFirstTime")]
    public bool IsFirstTime
    {
        get { return GetProperty<bool>("IsFirstTime"); }
        set { SetProperty<bool>(value, "IsFirstTime"); }
    }

    public void addCard(Card card)
    {
        cardList.Add(card);
    }

    public List<Card> getCardList()
    {
        return cardList;
    }

    public void CleanCardList()
    {
        cardList.Clear();
    }

    public static Deck createNewDeck()
    {
        Deck deck = new Deck();

        deck.DeckName = "";
        deck.UserId = Player.getInstance().UserId;
        deck.IsEditable = true;
        deck.TimesPlayed = 0;
        deck.IsFirstTime = true;

        return deck;
    }

}