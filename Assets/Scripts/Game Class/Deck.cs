using UnityEngine;
using Parse;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ParseClassName("Deck")]
public class Deck : ParseObject {

    private List<Card> cardList = new List<Card>();	          

	[ParseFieldName("DeckName")]
	public string DeckName {
		get { return GetProperty<string>("DeckName"); }
   		set { SetProperty<string>(value, "DeckName"); }
	}

    [ParseFieldName("UserId")]
    public string UserId
    {
        get { return GetProperty<string>("UserId"); }
        set { SetProperty<string>(value, "UserId"); }
    }

    [ParseFieldName("IsDefault")]
    public bool IsDefault
    {
        get { return GetProperty<bool>("IsDefault"); }
        set { SetProperty<bool>(value, "IsDefault"); }
    }

    public void addCard(Card card)
    {
        cardList.Add(card);
    }

    public IList<Card> getCardList()
    {
        return cardList;
    }

}