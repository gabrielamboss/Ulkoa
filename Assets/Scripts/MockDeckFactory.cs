using UnityEngine;
using System.Collections;

public class MockDeckFabric : MonoBehaviour
{

    public static Deck DefaultDeck()
    {
        Deck deck = new Deck();
        deck.TimesPlayed = 0;
        deck.IsFirstTime = true;

        deck.addCard(MockCard("front1", "back1", 1));
        deck.addCard(MockCard("front2", "back2", 1));
        deck.addCard(MockCard("front3", "back3", 2));
        deck.addCard(MockCard("front4", "back4", 4));
        deck.addCard(MockCard("front5", "back5", 5));
        deck.addCard(MockCard("front6", "back6", 1));
        deck.addCard(MockCard("front7", "back7", 2));
        deck.addCard(MockCard("front8", "back8", 5));
        deck.addCard(MockCard("front9", "back9", 1));
        deck.addCard(MockCard("front10", "back10", 1));
        deck.addCard(MockCard("front11", "back11", 1));
        deck.addCard(MockCard("front12", "back12", 4));

        return deck;
    }

    private static Card MockCard(string front, string back, int LeitnerLevel)
    {
        Card card = new Card();

        card.EnglishText = front;
        card.PortugueseText = back;
        card.LeitnerLevel = LeitnerLevel;


        return card;
    }

}
