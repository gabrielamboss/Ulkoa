using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System;
using System.Collections;

public abstract class Dao {

    protected bool wait;

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
            wait = false;
        }, (error) => {
            failedUserDataQuerry(error);
            wait = false;
        });

        while (wait)
        { yield return null; }
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
        throw new Exception(error.ErrorMessage);          
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
            wait = false;
        },
        (error) => {
            failedTitleDataQuerry(error);
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
        throw new Exception(error.ErrorMessage);
    }

    protected IEnumerator saveUserData(UpdateUserDataRequest request)
    {        
        wait = true;
        Debug.Log("Updating data");
        PlayFabClientAPI.UpdateUserData(request,
            (result) =>
            {
                succesfullSave(result);
                wait = false;
            },
            (error) =>
            {
                failedSave(error);
                wait = false;
            });

        while (wait)
        { yield return null; }
        Debug.Log("Finish saveplayer");
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

    
}
