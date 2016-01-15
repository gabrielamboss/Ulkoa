using UnityEngine;
using System.Collections;

public abstract class GlobalVariables{

    private static Deck selectedDeck;
	public static int correctAnswerAmount, wrongAnswerAmount;
	public static bool facebookLogin = false;
	public static bool normalLogin = false;

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
