using Parse;
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

    [ParseFieldName("DeckId")]
    public string DeckId
    {
        get { return GetProperty<string>("DeckId"); }
        set { SetProperty<string>(value, "DeckId"); }
    }
}