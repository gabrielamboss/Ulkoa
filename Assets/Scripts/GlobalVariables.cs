using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GlobalVariables{

    private static Deck selectedDeck;
	public static int correctAnswerAmount, wrongAnswerAmount;
	public static bool facebookLogin = false;
	public static bool normalLogin = false;
    public static int minCardsPerPlaySession = 10;
    public static bool continueGame = true;
    public static bool GoBackToGame = false;
    public static bool WasNotDisplayed = true;
    public static bool WasDisplayedEvolution = false;
    public static bool WasDisplayedHistory = false;
    public static bool WasDisplayedProgress = false;

    public static readonly List<List<int>> LeitnerRoutine = new List<List<int>>()
    {
                new List<int>() { 1 },
                new List<int>() { 1, 2 },
                new List<int>() { 1, 3 },
                new List<int>() { 1, 2 },
                new List<int>() { 1, 4 },
                new List<int>() { 1, 2 },
                new List<int>() { 1, 3 },
                new List<int>() { 1, 2 },
                new List<int>() { 1 },
                new List<int>() { 1, 2 },
                new List<int>() { 1, 3 },
                new List<int>() { 1, 2 },
                new List<int>() { 1, 5 },
                new List<int>() { 1, 2, 4 },
                new List<int>() { 1, 3 },
                new List<int>() { 1, 2 }
    };
    public static Dictionary<string, int> OriginalLevels;
    public static Dictionary<string, string> UserAnswers;

    public static void ResetCorrect(){
		correctAnswerAmount = 0;
	}

	public static void ResetWrong(){
		wrongAnswerAmount = 0;
	}

	public static void Reset(){
		ResetCorrect();
		ResetWrong();
	}

    public static void SetSelectedDeck(Deck deck)
    {
        selectedDeck = deck;
    }

    public static Deck GetSelectedDeck()
    {
        return selectedDeck;
    }
    
}
