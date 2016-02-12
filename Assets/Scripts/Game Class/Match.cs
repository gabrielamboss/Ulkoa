using System;
using UnityEngine;

[Serializable]
public class Match{
    
    public int CorrectPoints;    
    public int WrongPoints;
    public int MatchNumber;
    public string DeckID;

    public string ObjectId
    {
        get { return JsonUtility.ToJson(this); }
    }

}
