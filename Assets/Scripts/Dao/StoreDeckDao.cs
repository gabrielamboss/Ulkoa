using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class StoreDeckDao{

    List<StoreDeck> deckList = new List<StoreDeck>(); 

    public IEnumerator MakeQueryGetDeckList()
    {
        bool wait = true;

        GetTitleDataRequest getRequest = new GetTitleDataRequest();
        PlayFabClientAPI.GetTitleData(getRequest, (result) =>
        {
            Debug.Log("Got the following titleData:");
            foreach (var entry in result.Data)
            {
                Debug.Log(entry.Key + ": " + entry.Value);
                deckList.Add(JsonUtility.FromJson<StoreDeck>(entry.Value));
            }
            wait = false;
        },
        (error) => {
            Debug.Log("Got error getting titleData:");
            Debug.Log(error.ErrorMessage);
            wait = false;
        });

        while (wait)
        { yield return null; }
    }

    public List<StoreDeck> getQueryResultStoreDeckList()
    {
        return deckList;
    }
    
}
