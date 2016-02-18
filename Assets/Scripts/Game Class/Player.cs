using System;
using System.Collections;

[Serializable]
public class Player
{

    private static Player instance = null;    

    public string Username;    
    public int Currency;
    public bool IsPremium;
    public int PremiumCredit; 
    public StringListWrapper StoreDeckNameList = new StringListWrapper();
    public DeckListWrapper DeckList = new DeckListWrapper();

    public Player()
    {
        Currency = 10;
        IsPremium = false;
        PremiumCredit = 5;
    }

    public void addDeck(Deck deck)
    {
        if(!DeckList.Contains(deck))
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
    public static IEnumerator save()
    {
        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.savePlayer(getInstance());
    }
}
