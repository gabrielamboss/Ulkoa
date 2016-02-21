using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class MatchDao : Dao {

    private static MatchListWrapper matchList = new MatchListWrapper();

    public override IEnumerator makeQuerry()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        { Keys = new List<string>() { "MatchList" } };

        yield return userDataQuerry(request);
    }

    protected override void succesfullUserDataQuerry(GetUserDataResult result)
    {
        Dictionary<string, UserDataRecord> data = result.Data;
        if (data.ContainsKey("MatchList"))
            matchList = JsonUtility.FromJson<MatchListWrapper>(data["MatchList"].Value);
    }

    public IEnumerator saveMatch(Match match)
    {
        matchList.addMatch(match);

        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>(){
                {"MatchList", JsonUtility.ToJson(matchList)},
            }
        };

        yield return saveUserData(request);
    }

    public List<Match> getMatchsByDeck(Deck deck)
    {
        List<Match> answList = new List<Match>();

        foreach (Match match in matchList.getList())
        {
            if (match.DeckName.Equals(deck.DeckName))
                answList.Add(match);
        }

        return answList;
    }

    public List<Match> getMatchList()
    {
        return matchList.getList();
    }

}
