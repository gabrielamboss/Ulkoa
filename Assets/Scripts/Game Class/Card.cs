using Parse;
using System;
using UnityEngine;
using System.Collections;

[ParseClassName("Card")]
public class Card : ParseObject {    

	[ParseFieldName("PortugueseText")]
  	public string PortugueseText{
		get { return GetProperty<string>("PortugueseText"); }
   		set { SetProperty<string>(value, "PortugueseText"); }
	}

	[ParseFieldName("EnglishText")]
	public string EnglishText{
		get { return GetProperty<string>("EnglishText"); }
   		set { SetProperty<string>(value, "EnglishText"); }
	}

    [ParseFieldName("UserId")]
    public string UserId
    {
        get { return GetProperty<string>("UserId"); }
        set { SetProperty<string>(value, "UserId"); }
    }

    [ParseFieldName("DeckId")]
    public string DeckId
    {
        get { return GetProperty<string>("DeckId"); }
        set { SetProperty<string>(value, "DeckId"); }
    }

    [ParseFieldName("LeitnerLevel")]
    public int LeitnerLevel
    {
        get { return GetProperty<int>("LeitnerLevel"); }
        set { SetProperty<int>(value, "LeitnerLevel"); }
    }    

    public static Card creatNewCard()
    {
        Card card = new Card();

        card.PortugueseText = "NovoPor";
        card.EnglishText = "NewEngl";
        card.LeitnerLevel = 1;

        return card;
    }
}