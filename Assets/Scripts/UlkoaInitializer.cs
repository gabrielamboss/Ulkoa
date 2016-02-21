using PlayFab;
using System.Collections;
using System.Collections.Generic;

public abstract class UlkoaInitializer {

    private static bool succsesfull;
    private static PlayFabError error;

    public static IEnumerator InitializeGame()
    {
        //Making query
        PlayerDao playerDao = new PlayerDao();
        StoreDeckDao storeDeckDao = new StoreDeckDao();
        MatchDao matchDao = new MatchDao();

        List<Dao> daoList = new List<Dao>() { playerDao, storeDeckDao, matchDao };
        yield return makeQuerry(daoList);

        if (!succsesfull)
            yield break;

        //Getting results
        Player player = playerDao.getQueryResultPlayer();
        Player.setInstance(player);

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

    }

    public static IEnumerator makeQuerry(List<Dao> daoList)
    {
        succsesfull = true;
        foreach (Dao dao in daoList)
        {
            if (succsesfull)
            {
                yield return dao.makeQuerry();

                if (!dao.isQuerrySuccessfull())
                {
                    succsesfull = false;
                    error = dao.getError();
                }
            }
        }
    }

    public static bool isSuccessfull()
    {
        return succsesfull;
    }

    public static PlayFabError getError()
    {
        return error;
    }

}
