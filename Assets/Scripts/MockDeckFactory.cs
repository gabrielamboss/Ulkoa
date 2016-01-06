using UnityEngine;
using System.Collections;

public class MockDeckFabric : MonoBehaviour {

	public static Deck DefaultDeck()
    {
        Deck deck = new Deck();

        deck.addCard(MockCard("front1", "back1"));
        deck.addCard(MockCard("front2", "back2"));
        deck.addCard(MockCard("front3", "back3"));
        deck.addCard(MockCard("front4", "back4"));

        return deck;
    }

    private static Card MockCard(string front, string back)
    {
        Card card = new Card();

        card.FrontText = front;
        card.BackText = back;

        return card;
    }

}
