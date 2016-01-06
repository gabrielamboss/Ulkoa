using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDeckMannager {// : MonoBehaviour {

    private Deck deck;
    private Queue<Card> cardQueue;

    public GameDeckMannager(Deck deck)
    {
        this.deck = deck;

        cardQueue = new Queue<Card>(deck.getCardList());
    }

    public Card GetNextCard()
    {
        return cardQueue.Dequeue();
    }

    public bool IsQueueEmpty()
    {
        return (cardQueue.Count == 0);
    }
}
