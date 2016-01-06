using UnityEngine;
using Parse;
using System.Collections;
using System.Collections.Generic;

[ParseClassName("Deck")]
public class Deck : ParseObject {

    private IList<Card> cardList = new List<Card>();

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

    public void addCard(Card card)
    {
        cardList.Add(card);
    }

    public IList<Card> getCardList()
    {
        return cardList;
    }

	[ParseFieldName("DeckName")]
	public string DeckName {
		get { return GetProperty<string>("DeckName"); }
   		set { SetProperty<string>(value, "DeckName"); }
	}

	[ParseFieldName("Description")]
	public string Description {
		get { return GetProperty<string>("Description"); }
   		set { SetProperty<string>(value, "Description"); }
	}

	[ParseFieldName("Relevancy")]
	public string Relevancy {
		get { return GetProperty<string>("Relevancy"); }
   		set { SetProperty<string>(value, "Relevancy"); }
	}

	[ParseFieldName("CardArt")]
	public string CardArt {
		get { return GetProperty<string>("CardArt"); }
   		set { SetProperty<string>(value, "CardArt"); }
	}

	[ParseFieldName("CardIds")]
	public IList<string> CardIds {
		get { return GetProperty<IList<string>>("CardIds"); }
		set { SetProperty<IList<string>>(value, "CardIds"); }
	}

}