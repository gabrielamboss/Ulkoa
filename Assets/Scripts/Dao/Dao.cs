using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System;
using System.Collections;

public abstract class Dao {

    protected bool wait;
    private bool querrySuccesfull;
    private bool saveSuccesfull;
    private PlayFabError error;

    public abstract IEnumerator makeQuerry();

    protected IEnumerator userDataQuerry(GetUserDataRequest request)
    {
        wait = true;

        PlayFabClientAPI.GetUserData(request, (result) => {
            Debug.Log("Got user data:");
            if ((result.Data == null) || (result.Data.Count == 0))
            {
                Debug.Log("No user data available");
            }
            else
            {
                succesfullUserDataQuerry(result);
            }
            querrySuccesfull = true;
            wait = false;
        }, (error) => {
            failedUserDataQuerry(error);
            this.error = error;
            querrySuccesfull = false;
            wait = false;
        });

        while (wait)
        { yield return null; }
    }

    public bool isQuerrySuccessfull()
    {
        return querrySuccesfull;
    }

    protected virtual void succesfullUserDataQuerry(GetUserDataResult result)
    {
        Debug.Log("Got the following userData:");
        foreach (var entry in result.Data)
        {
            Debug.Log(entry.Key + ": " + entry.Value);
        }
    }

    protected virtual void failedUserDataQuerry(PlayFabError error)
    {
        Debug.Log("Got error retrieving user data:");
    }

    protected IEnumerator titleDataQuerry(GetTitleDataRequest request)
    {
        wait = true;

        PlayFabClientAPI.GetTitleData(request, (result) =>
        {
            Debug.Log("Got title data:");
            if ((result.Data == null) || (result.Data.Count == 0))
            {
                Debug.Log("No title data available");
            }
            else
            {
                succesfullTitleDataQuerry(result);
            }
            querrySuccesfull = true;
            wait = false;
        },
        (error) => {
            failedTitleDataQuerry(error);
            this.error = error;
            querrySuccesfull = false;
            wait = false;
        });

        while (wait)
        { yield return null; }
    }

    protected virtual void succesfullTitleDataQuerry(GetTitleDataResult result)
    {
        Debug.Log("Got the following titleData:");
        foreach (var entry in result.Data)
        {
            Debug.Log(entry.Key + ": " + entry.Value);
        }
    }

    protected virtual void failedTitleDataQuerry(PlayFabError error)
    {
        Debug.Log("Got error retrieving title data:");
    }

    protected IEnumerator saveUserData(UpdateUserDataRequest request)
    {
        wait = true;
        Debug.Log("Updating data");
        PlayFabClientAPI.UpdateUserData(request,
            (result) =>
            {
                succesfullSave(result);
                saveSuccesfull = true;
                wait = false;
            },
            (error) =>
            {
                failedSave(error);
                this.error = error;
                saveSuccesfull = false;
                wait = false;
            });

        while (wait)
        { yield return null; }
        Debug.Log("Finish saveplayer");
    }

    public bool isSaveSuccessfull()
    {
        return saveSuccesfull;
    }

    protected virtual void succesfullSave(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated user data");
    }

    protected virtual void failedSave(PlayFabError error)
    {
        Debug.Log("Got error setting user data Ancestor to Arthur");
        Debug.Log(error.ErrorDetails);
    }

    public PlayFabError getError()
    {
        return error;
    }


}
