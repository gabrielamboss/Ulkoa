﻿using UnityEngine;
using System.Collections;
using ProgressBar;
using System;
using System.Collections.Generic;

public class DeckProgress : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInParent<ProgressRadialBehaviour>().SetFillerSizeAsPercentage(DetermineCurrentDeckProgress());

    }

    private float DetermineCurrentDeckProgress()
    {
        Deck currentDeck = GlobalVariables.GetSelectedDeck();
        List<Card> currentCards = currentDeck.cardList;
        int cardQuantity = currentCards.Count;
        float maxProgressPerCard = 100 / cardQuantity;
        float deckProgress = 0;
        foreach (Card card in currentCards)
        {
            float cardProgress;
            cardProgress = (card.LeitnerLevel - 1) * maxProgressPerCard / 4;
            Debug.Log(card.EnglishText + "ja esta com " + cardProgress + " de progresso. Atual LeitnerLevel: " + card.LeitnerLevel);
            deckProgress += cardProgress;
        }
        return deckProgress;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
