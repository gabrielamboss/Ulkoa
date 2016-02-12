using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerDao {

    private Player player;

    public IEnumerator MakeQueryGetPlayer()
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
                player = new Player();

                Dictionary<string,UserDataRecord> data = result.Data;                
                player.Username = data["UserName"].Value;                
                player.Currency = Int32.Parse(data["Currency"].Value);                
                player.IsPremium = Boolean.Parse(data["IsPremium"].Value);
                player.StoreDeckNameList = JsonUtility.FromJson<StringListWrapper>(data["StoreDeckNameList"].Value);
                player.DeckList = JsonUtility.FromJson<DeckListWrapper>(data["DeckList"].Value);
            }
            wait = false;
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.ErrorMessage);
            wait = false;
        });

        while (wait)
        {yield return null;}

    }    

    public Player getQueryResultPlayer()
    {
        return player;
    }

    public IEnumerator savePlayer(Player player)
    {
        Debug.Log("Cerating request");
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {           
            Data = new Dictionary<string,string>(){
                {"UserName", player.Username},
                {"Currency", player.Currency.ToString()},
                {"IsPremium", player.IsPremium.ToString()},
                {"StoreDeckNameList", JsonUtility.ToJson(player.StoreDeckNameList)},
                {"DeckList", JsonUtility.ToJson(player.DeckList)},
            }
        };
        Debug.Log("Request is done");
        bool wait = true;
        Debug.Log("Updating data");
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
        Debug.Log("Fisnish saveplayer");
    }
	
}
