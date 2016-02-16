using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchDao {

    private static MatchListWrapper matchList = new MatchListWrapper();

    public IEnumerator MakeQueryGetMatchList()
    {
        bool wait = true;

        GetUserDataRequest request = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData(request, (result) => {
            Debug.Log("Got user data:");
            if ((result.Data == null) || (result.Data.Count == 0))
            {
                Debug.Log("No user data available");
            }
            else
            {                
                Dictionary<string, UserDataRecord> data = result.Data;
                if(data.ContainsKey("MatchList"))
                    matchList = JsonUtility.FromJson<MatchListWrapper>(data["MatchList"].Value);
            }
            wait = false;
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.ErrorMessage);
            wait = false;
        });

        while (wait)
        { yield return null; }

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
    
    public IEnumerator saveMatch(Match match)
    {
        bool wait = true;
        matchList.addMatch(match);

        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>(){                
                {"MatchList", JsonUtility.ToJson(matchList)},
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            (result) =>
            {
                Debug.Log("Successfully updated user data");
                wait = false;
            },
            (error) =>
            {
                Debug.Log("Got error setting user data");
                Debug.Log(error.ErrorDetails);
                wait = false;
            });

        while (wait)
        { yield return null; }
    }    
}
