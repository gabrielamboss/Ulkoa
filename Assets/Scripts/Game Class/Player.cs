using System;

[Serializable]
public class Player
{

    private static Player instance = null;    

    public string Username;    
    public int Currency;
    public bool IsPremium;    
    public StringListWrapper StoreDeckNameList = new StringListWrapper();
    public DeckListWrapper DeckList = new DeckListWrapper();

    public Player()
    {
        Currency = 10;
        IsPremium = false;
    }

    public void addDeck(Deck deck)
    {
        DeckList.Add(deck);
    }
    public void removeDeck(Deck deck)
    {
        DeckList.Remove(deck);
    }      
    public bool hasDeck(Deck deck)
    {
        return DeckList.Contains(deck);        
    }    

    public void addToStoreDeckNameList(String sdName)
    {
        StoreDeckNameList.Add(sdName);
    }

    public static void setInstance(Player player)
    {
        instance = player;
    }
    public static Player getInstance()
    {
        return instance;
    }
    public static void save()
    {
        PlayerDao playerDao = new PlayerDao();
        playerDao.savePlayer(getInstance());
    }
}
