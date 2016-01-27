using Parse;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ParseClassName("Player")]
public class Player : ParseObject
{

    private static Player instance = null;

    //Obs.: acho que o construtor nao poder ser private por causa da 
    //heranca de ParseObject mas ainda nao testei (quase certeza pq ele 
    //precisa ser construido quando chega do banco de dados)

    private string playerName;
    private List<Deck> deckList = new List<Deck>();

    public void setName(string name)
    {
        playerName = name;
    }
    public string getName()
    {
        return playerName;
    }

    [ParseFieldName("UserId")]
    public string UserId
    {
        get { return GetProperty<string>("UserId"); }
        set { SetProperty<string>(value, "UserId"); }
    }

    [ParseFieldName("Currency")]
    public int Currency
    {
        get { return GetProperty<int>("Currency"); }
        set { SetProperty<int>(value, "Currency"); }
    }

    [ParseFieldName("IsPremium")]
    public bool IsPremium
    {
        get { return GetProperty<bool>("IsPremium"); }
        set { SetProperty<bool>(value, "IsPremium"); }
    }

    [ParseFieldName("StoreDeckNameList")]
    public IList<string> StoreDeckNameList
    {
        get { return GetProperty<IList<string>>("StoreDeckNameList"); }
        set { SetProperty<IList<string>>(value, "StoreDeckNameList"); }
    }

    public void addDeck(Deck deck)
    {
        deckList.Add(deck);
    }
    public void removeDeck(Deck deck)
    {
        deckList.Remove(deck);
    }

    public List<Deck> getDeckList()
    {
        return deckList;
    }

    public bool hasDeck(Deck deck2)
    {
        bool answ = false;

        foreach (Deck deck in deckList)
        {
            if (deck.Equals(deck2))
                answ = true;
        }

        return answ;
    }

    public static void setInstance(Player player)
    {
        instance = player;
    }
    public static Player getInstance()
    {
        return instance;
    }
}
