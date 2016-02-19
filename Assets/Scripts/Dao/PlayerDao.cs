 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;

public class PlayerDao : Dao{
    
    private Player player = new Player();

    public IEnumerator MakeQueryGetPlayer()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        {
            Keys = new List<string>()
            {
                "UserName",
                "Currency",
                "IsPremium",
                "PremiumCredit",
                "StoreDeckNameList",
                "DeckList"
            }
        };

        yield return userDataQuerry(request);        
    }

    protected override void succesfullUserDataQuerry(GetUserDataResult result)
    {
        Dictionary<string, UserDataRecord> data = result.Data;
        player.Username = data["UserName"].Value;
        player.Currency = Int32.Parse(data["Currency"].Value);
        player.IsPremium = Boolean.Parse(data["IsPremium"].Value);
        player.PremiumCredit = Int32.Parse(data["PremiumCredit"].Value);
        player.StoreDeckNameList = JsonUtility.FromJson<StringListWrapper>(data["StoreDeckNameList"].Value);
        player.DeckList = JsonUtility.FromJson<DeckListWrapper>(data["DeckList"].Value);
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
                {"PremiumCredit", player.PremiumCredit.ToString()},
                {"StoreDeckNameList", JsonUtility.ToJson(player.StoreDeckNameList)},
                {"DeckList", JsonUtility.ToJson(player.DeckList)},
            }
        };
        Debug.Log("Request is done");

        yield return saveUserData(request);

    }

    
}
