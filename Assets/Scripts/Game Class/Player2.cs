using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System;
using System.Linq;

public class Player2{

    private static ParseUser user;
    private static List<Deck> deckList = new List<Deck>();
    private static List<Card> cardList = new List<Card>();
    private static bool initialized = false;

    public static void Init()
    {
        ParseObject.RegisterSubclass<Deck>();        
        Debug.Log("Initializing player:");
        user = ParseUser.CurrentUser;
        if(user != null)
        {
            ParseQuery<Deck> queryDeck = new ParseQuery<Deck>().WhereEqualTo("UserId",user.ObjectId);            
            queryDeck.FindAsync().ContinueWith(t=>
            {                
                if (t.Exception != null || t.IsCanceled || t.IsFaulted)
                {
                    Debug.Log("Deu merda");
                }
                else
                {                    
                    deckList = t.Result.ToList<Deck>();
                    Debug.Log("Deck Query has finished, total decks found: " + deckList.Count());

                    InitCards();
                }                                
            });     
        }
        else
        {
            Debug.Log("You forget to connect");
        }
    }

    private static void InitCards()
    {
        ParseObject.RegisterSubclass<Card>();
        ParseQuery<Card> queryCard = new ParseQuery<Card>().WhereEqualTo("UserId", user.ObjectId);        
        
        queryCard.FindAsync().ContinueWith(t =>
        {
            if (t.IsCanceled || t.IsFaulted)
            {
                Debug.Log("Deu merda");
                Debug.Log(t.Exception);
                Debug.Log(t.IsCanceled);
            }
            else
            {
                cardList = t.Result.ToList<Card>();
                Debug.Log("Card Query has finished, total cards found: " + cardList.Count());
                BuildDecks();
            }                                
        });                
    }

    private static void BuildDecks()
    {        
        Debug.Log("Start to build decks");

        Dictionary<string, Deck> dec = new Dictionary<string, Deck>();
        foreach (Deck deck in deckList)
        {
            dec[deck.ObjectId] = deck;            
        }

        foreach (Card card in cardList)
        {
            dec[card.DeckId].addCard(card);
        }

        initialized = true;
        Debug.Log("Build deck has finish");
    }

    public static bool IsInitialized()
    {
        return initialized;
    }

    public static List<Deck> GetDeckList()
    {
        return deckList;
    }

    public static int GetTotalDecks()
    {
        return deckList.Count();
    }

    public static string GetPlayerName()
    {
        return user.Username;
    }

    public static void AddDeck(Deck newDeck)
    {
        bool find = false;
        foreach (Deck deck in deckList)
        {
            if (deck.Equals(newDeck))
            {
                find = true;
            }
        }

        if (!find)
        {
            deckList.Add(newDeck);
        }
    }

}
