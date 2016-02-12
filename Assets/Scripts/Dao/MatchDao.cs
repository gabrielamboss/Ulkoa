using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchDao {

    List<Match> matchList;

    public IEnumerator MakeQueryGetMatchList(Deck deck)
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
                matchList = JsonUtility.FromJson<List<Match>>(data["MatchList"].Value);
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

    public List<Match> getQueryResultMatchList()
    {
        return matchList;
    }
    
    public IEnumerator saveMatch(Match match)
    {
        bool wait = true;

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
                Debug.Log("Got error setting user data Ancestor to Arthur");
                Debug.Log(error.ErrorDetails);
                wait = false;
            });

        while (wait)
        { yield return null; }
    }    
}
