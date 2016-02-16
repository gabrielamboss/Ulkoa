using System;
using UnityEngine;

[Serializable]
public class Card {

    public string PortugueseText;    
    public string EnglishText;
    public int LeitnerLevel;

    public Card()
    {
        PortugueseText = "NovoPor";
        EnglishText = "NewEngl";
        LeitnerLevel = 1;
    }
    
    public string ObjectId
    {
        get { return PortugueseText + EnglishText; }
    }    
}