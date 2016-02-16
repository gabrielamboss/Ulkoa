using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MatchListWrapper{

    [SerializeField]
    private List<Match> matchList = new List<Match>();

    public void addMatch(Match match)
    {
        matchList.Add(match);
    }
	
    public List<Match> getList()
    {
        return matchList;
    }

}
