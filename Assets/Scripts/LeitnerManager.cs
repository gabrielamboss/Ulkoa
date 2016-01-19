using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LeitnerManager : SRSManager{
    private Deck deck;
    private List<Card> cardList;
    private List<Card> currentDeck = new List<Card>();
    public int MockTimesPlayed = 10;
    private int currentTimesPlayed;
    private int numberOfCurrentTraining;
    private List<int> currentTraining;
    private List<Card> sessionCards = new List<Card>();
    

    public LeitnerManager(Deck deck)
    {
        this.deck = deck;
        cardList = (List<Card>) deck.getCardList();
        Debug.Log("LetnerManager started");
        //foreach (Card card in cardList)
        //   {
        //        Debug.Log(c   ard.LetnerLevel);
        //    }

        setUpCurrentDeck();
     
    }

    private void setUpCurrentDeck()
    {
        currentTimesPlayed = deck.TimesPlayed;
        numberOfCurrentTraining = currentTimesPlayed % GlobalVariables.LeitnerRoutine.Count;
        currentTraining = GlobalVariables.LeitnerRoutine[numberOfCurrentTraining];

        foreach (Card card in cardList)
        {
            if (currentTraining.Contains(card.LeitnerLevel))
            {
                if (!currentDeck.Contains(card))
                {
                    currentDeck.Add(card);
                    Debug.Log(card.LeitnerLevel);
                }
            }
        }
        while (currentDeck.Count < GlobalVariables.minCardsPerPlaySession || currentDeck.Count == deck.getCardList().Count)
        {
            deck.TimesPlayed++;
            setUpCurrentDeck();

        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Card GetNextCard()
    {

        int currentSize = currentDeck.Count;
        int randomNextCard = new System.Random().Next(0, currentSize);
        Debug.Log("Random" + randomNextCard);
        Card cardRemoved = currentDeck[randomNextCard];
        currentDeck.Remove(cardRemoved);
        sessionCards.Add(cardRemoved);
        return cardRemoved;
    }

    public bool IsCurrentDeckEmpty()
    {
        if (currentDeck.Count <= 0) return true;
        else return false;
    }

    public List<Card> GetCurrentWaitingDeck()
    {
        return currentDeck;
    }

    public List<Card> GetSessionCards()
    {
        return sessionCards;
    }
}
