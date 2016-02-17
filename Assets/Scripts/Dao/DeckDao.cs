using UnityEngine;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class DeckDao : Dao {

    private DeckListWrapper deckList = new DeckListWrapper();

    public IEnumerator MakeQueryGetPlayer()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        { Keys = new List<string>(){ "DeckList" } };

        yield return userDataQuerry(request);
    }

    protected override void succesfullUserDataQuerry(GetUserDataResult result)
    {
        Dictionary<string, UserDataRecord> data = result.Data;        
        deckList = JsonUtility.FromJson<DeckListWrapper>(data["DeckList"].Value);
    }

    public List<Deck> getQueryResultDeckList()
    {
        return deckList.getList();
    }

    public IEnumerator saveDeck(Deck deck)
    {
        Player player = Player.getInstance();
        deckList = player.DeckList;

        if (!deckList.Contains(deck))
            deckList.Add(deck);

        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>(){                
                {"DeckList", JsonUtility.ToJson(deckList)},
            }
        };

        yield return saveUserData(request);
    }

}
