using UnityEngine;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class StoreDeckDao : Dao{

    List<StoreDeck> deckList = new List<StoreDeck>();

    public override IEnumerator makeQuerry()
    {
        GetTitleDataRequest request = new GetTitleDataRequest();
        yield return titleDataQuerry(request);
    }

    protected override void succesfullTitleDataQuerry(GetTitleDataResult result)
    {
        foreach (var entry in result.Data)
        {
            deckList.Add(JsonUtility.FromJson<StoreDeck>(entry.Value));
        }
    }

    public List<StoreDeck> getQueryResultStoreDeckList()
    {
        return deckList;
    }

}
