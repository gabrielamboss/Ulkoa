using Parse;
using System.Collections;
using System.Collections.Generic;

public abstract class UlkoaInitializer {

    private static bool hasInitialized = false;    

    public static IEnumerator InitializeGame()
    {                                        
        ParseUser user = ParseUser.CurrentUser;
        
        //Making query
        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.MakeQueryGetPlayer(user);
        Player player = playerDao.getQueryResultPlayer();
        
        DeckDao deckDao = new DeckDao();
        yield return deckDao.MakeQueryGetDeckList(player);
        List<Deck> deckList = deckDao.getQueryResultDeckList();

        StoreDeckDao storeDeckDao = new StoreDeckDao();
        yield return storeDeckDao.MakeQueryGetDeckList();
        List<StoreDeck> storeDeckList = storeDeckDao.getQueryResultStoreDeckList();

        //Injecting dependency
        player.setName(user.Username);
        player.setDeckList(deckList);
        Player.setInstance(player);

        bool change = false;
        DeckBuilder deckBuilder;
        Deck deck;
        IList<string> playerStoreDecks = player.StoreDeckNameList;
        foreach (StoreDeck storeDeck in storeDeckList)
        {           
            if (!playerStoreDecks.Contains(storeDeck.DeckName))
            {
                if (player.IsPremium)
                {
                    deckBuilder = new DeckBuilder(storeDeck);
                    deck = deckBuilder.getDeck();
                    yield return deckDao.saveDeck(deck);
                    player.addDeck(deck);
                    player.AddToList("StoreDeckNameList", deck.DeckName);
                    change = true;
                }
                else
                {
                    Store.addDeck(storeDeck);
                }                
            }
        }

        if (change)
        {
            playerDao.savePlayer(player);
        }

        hasInitialized = true;
    }        

    public static bool HasInitialized()
    {
        return hasInitialized;
    }
    
}
