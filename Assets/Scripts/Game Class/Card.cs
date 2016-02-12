using System;
using UnityEngine;

[Serializable]
public class Card {

    public string PortugueseText;    
    public string EnglishText;
    public int LeitnerLevel;

    public string ObjectId
    {
        get { return JsonUtility.ToJson(this); }
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