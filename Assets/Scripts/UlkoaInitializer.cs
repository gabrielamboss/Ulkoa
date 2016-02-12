using System.Collections;
using System.Collections.Generic;

public abstract class UlkoaInitializer {

    private static bool hasInitialized = false;    

    public static IEnumerator InitializeGame()
    {        
        //Making query
        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.MakeQueryGetPlayer();
        Player player = playerDao.getQueryResultPlayer();
        Player.setInstance(player);        

        StoreDeckDao storeDeckDao = new StoreDeckDao();
        yield return storeDeckDao.MakeQueryGetDeckList();
        List<StoreDeck> storeDeckList = storeDeckDao.getQueryResultStoreDeckList();        

        bool change = false;
        DeckBuilder deckBuilder;
        Deck deck;
        List<string> playerStoreDecks = player.StoreDeckNameList.getList();
        foreach (StoreDeck storeDeck in storeDeckList)
        {           
            if (!playerStoreDecks.Contains(storeDeck.DeckName))
            {
                if (player.IsPremium)
                {
                    deckBuilder = new DeckBuilder(storeDeck);
                    deck = deckBuilder.getDeck();                    
                    player.addDeck(deck);
                    player.addToStoreDeckNameList(deck.DeckName);
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
