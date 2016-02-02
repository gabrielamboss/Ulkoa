using Parse;
using System.Collections;
using System.Collections.Generic;

public abstract class UlkoaInitializer {

    private static bool hasInitialized = false;    

    public static IEnumerator InitializeGame()
    {                                        
        ParseUser user = ParseUser.CurrentUser;
        
        //Making querry
        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.MakeQuerryGetPlayer(user);
        Player player = playerDao.getQuerryResultPlayer();
        
        DeckDao deckDao = new DeckDao();
        yield return deckDao.MakeQuerryGetDeckList(player);
        List<Deck> deckList = deckDao.getQuerryResultDeckList();

        StoreDeckDao storeDeckDao = new StoreDeckDao();
        yield return storeDeckDao.MakeQuerryGetDeckList();
        List<StoreDeck> storeDeckList = storeDeckDao.getQuerryResultStoreDeckList();

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

    public static bool hasInitialied()
    {
        return hasInitialized;
    }
    
}
