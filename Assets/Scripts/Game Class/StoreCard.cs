using Parse;
using UnityEngine;
using System.Collections;

[ParseClassName("StoreCard")]
public class StoreCard : ParseObject {

    [ParseFieldName("DeckId")]
    public string StoreDeckId
    {
        get { return GetProperty<string>("StoreDeckId"); }
        set { SetProperty<string>(value, "StoreDeckId"); }
    }

    [ParseFieldName("PortugueseText")]
    public string PortugueseText
    {
        get { return GetProperty<string>("PortugueseText"); }
        set { SetProperty<string>(value, "PortugueseText"); }
    }

    [ParseFieldName("EnglishText")]
    public string EnglishText
    {
        get { return GetProperty<string>("EnglishText"); }
        set { SetProperty<string>(value, "EnglishText"); }
    }
}
