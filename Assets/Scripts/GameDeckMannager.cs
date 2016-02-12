using System.Collections.Generic;

public class GameDeckMannager {

    private Deck deck;
    private Queue<Card> cardQueue;

    public GameDeckMannager(Deck deck)
    {
        this.deck = deck;

        cardQueue = new Queue<Card>();
        foreach(Card card in deck.cardList)
        {
            cardQueue.Enqueue(card);
        }
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
