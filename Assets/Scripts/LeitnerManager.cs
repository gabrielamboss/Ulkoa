using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LeitnerManager : SRSManager
{
    private Deck deck;
    private List<Card> cardList;
    private List<Card> currentDeck = new List<Card>();
    public int MockTimesPlayed = 10;
    private int currentTimesPlayed;
    private int numberOfCurrentTraining;
    private int originalSize;
    private List<int> currentTraining;
    private List<Card> sessionCards = new List<Card>();


    public LeitnerManager(Deck deck)
    {
        this.deck = deck;
        cardList = (List<Card>)deck.cardList;
        GlobalVariables.OriginalLevels = new Dictionary<string, int>();
        GlobalVariables.UserAnswers = new Dictionary<string, string>();
        Debug.Log("LetnerManager started");
        //foreach (Card card in cardList)
        //   {
        //        Debug.Log(card.LetnerLevel);
        //    }

        setUpCurrentDeck();

    }

    private void setUpCurrentDeck()
    {
        currentTimesPlayed = deck.SessionNumber;
        Debug.Log("Leitner manager: Sessao numero " + currentTimesPlayed);
        numberOfCurrentTraining = currentTimesPlayed % GlobalVariables.LeitnerRoutine.Count;
        currentTraining = GlobalVariables.LeitnerRoutine[numberOfCurrentTraining];

        foreach (Card card in cardList)
        {
            if (currentTraining.Contains(card.LeitnerLevel))
            {
                if (!currentDeck.Contains(card))
                {
                    currentDeck.Add(card);
                    Debug.Log("Carta adicionada: " + card.PortugueseText);
                    //Vamo ter que mudar isso
                    //GlobalVariables.OriginalLevels.Add(card.ObjectId, card.LeitnerLevel);
                    //Debug.Log("Testando dicionario. Leitner level: " + GlobalVariables.OriginalLevels[card.ObjectId]);
                    //Debug.Log("Carta " + card.EnglishText + " com LeitnerLevel " + card.LeitnerLevel + " adicionada");
                }
            }
        }
        deck.SessionNumber++;

        if (currentDeck.Count <= deck.cardList.Count / 2)
        {
            Debug.Log("Deck atual possui " + currentDeck.Count + " cartas. Adicionando mais cartas");
            setUpCurrentDeck();
        }
        originalSize = currentDeck.Count;

    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Card GetNextCard()
    {

        int currentSize = currentDeck.Count;
        int randomNextCard = new System.Random().Next(0, currentSize);
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

    public bool IsFirst()
    {
        if (currentDeck.Count == originalSize) return true;
        else return false;
    }
}
