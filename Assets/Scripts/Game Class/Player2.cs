using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System;
using System.Linq;

public class Player2 {

    private static ParseUser user;
    private static List<Deck> deckList = new List<Deck>();

	public static void Init()
    {
        ParseObject.RegisterSubclass<Deck>();
        Debug.Log("Initializing player:");
        user = ParseUser.CurrentUser;
        if(user != null)
        {
            ParseQuery<Deck> query = new ParseQuery<Deck>().WhereEqualTo("UserId",user.ObjectId);            
            query.FindAsync().ContinueWith(t=>
            {                
                if (t.Exception != null || t.IsCanceled || t.IsCanceled)
                {
                    Debug.Log("Deu merda");
                }
                else
                {                    
                    deckList = t.Result.ToList<Deck>();
                    Debug.Log("Deck Query has finished, total decks found: " + deckList.Count());
                    foreach (Deck deck in deckList)
                    {
                        deck.Init();
                    }
                }                                
            });
        }
        else
        {
            Debug.Log("You forgot to connect");
        }
    }

    public static List<Deck> GetDeckList()
    {
        return deckList;
    }

    public static int GetTotalDecks()
    {
        return deckList.Count();
    }

}
