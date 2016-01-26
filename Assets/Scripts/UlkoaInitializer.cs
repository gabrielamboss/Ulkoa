using Parse;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class UlkoaInitializer {

    private static bool hasInitialized = false;
    private static System.Object result;

    public static IEnumerator InitializeGame()
    {
        Debug.Log("Start initializer");

        Player player = null;
        List<Deck> deckList = null;
        List<Card> cardList = null;
        List<StoreDeck> storeDeckList = null;
        List<StoreCard> storeCardList = null;
        ParseUser user = ParseUser.CurrentUser;
        bool wait;

        //Initialize Player
        Debug.Log("Initializing Player");
        yield return Querry<Player>(new ParseQuery<Player>().WhereEqualTo("UserId", user.ObjectId));
        player = (result as List<Player>)[0];
        if (player == null) Debug.Log("Fuck achei"); 

        //Initialize Deck        
        yield return Querry<Deck>(new ParseQuery<Deck>().WhereEqualTo("UserId", user.ObjectId));
        deckList = result as List<Deck>;        

        //Initialize Cards        
        yield return Querry<Card>(new ParseQuery<Card>().WhereEqualTo("UserId", user.ObjectId));
        cardList = result as List<Card>;        

        //Build player and decks
        player.setName(user.Username);
        Player.setInstance(player);

        Dictionary<string, Deck> dict = new Dictionary<string, Deck>();
        foreach (Deck deck in deckList)
        {
            dict[deck.ObjectId] = deck;
            player.addDeck(deck);
        }

        foreach (Card card in cardList)
        {
            dict[card.DeckId].addCard(card);
        }

        //Initialize Store
        yield return Querry<StoreDeck>(new ParseQuery<StoreDeck>());
        storeDeckList = result as List<StoreDeck>;

        yield return Querry<StoreCard>(new ParseQuery<StoreCard>());
        storeCardList = result as List<StoreCard>;

        //Build store decks
        Dictionary<string, StoreDeck> dict2 = new Dictionary<string, StoreDeck>();        
        IList<string> playerStoreDecks = player.StoreDeckNameList;        
        foreach (StoreDeck deck in storeDeckList)
        {
            dict2[deck.ObjectId] = deck;            
            if (!playerStoreDecks.Contains(deck.DeckName))
            {
                Debug.Log("Player nao possui: " + deck.DeckName);
                Store.addDeck(deck);
            }
            else
            {
                Debug.Log("Player possui: " + deck.DeckName);
            }
                
        }

        foreach (StoreCard card in storeCardList)
        {
            dict2[card.StoreDeckId].addCard(card);
        }        

        hasInitialized = true;
    }

    private static IEnumerator Querry<T>(ParseQuery<T> querry) where T : ParseObject
    {       
        bool wait = true;
        Action<Task<IEnumerable<T>>> a = t => {            
            if (t.IsCanceled || t.IsFaulted)
            {
                Debug.Log("Initializing problem");
                Debug.Log(t.Exception);
                //Do something
            }
            else
            {
                result = t.Result.ToList<T>();
            }
            wait = false;
        };
        querry.FindAsync().ContinueWith(a);
        while (wait)
        {
            yield return null;
        }
    }    

    public static bool hasInitialied()
    {
        return hasInitialized;
    }

}
