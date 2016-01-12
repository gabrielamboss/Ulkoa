using Parse;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Text cardLabel;
    public InputField userInput;
    public bool test;
    private Card currentCard;
    private GameDeckMannager deckMannager;
    private LevelManager levelManager;

    void Awake()
    {
        ParseObject.RegisterSubclass<Card>();
        ParseObject.RegisterSubclass<Deck>();
        ParseObject.RegisterSubclass<CardCollection>();
    }

    // Use this for initialization
    void Start () {
    	GlobalVariables.correctAnswerAmount = 0;
    	GlobalVariables.wrongAnswerAmount = 0;
        if (test)
        {
            Debug.Log("Test");
            deckMannager = new GameDeckMannager(MockDeckFabric.DefaultDeck());           
        }
        else
        {
            Debug.Log("Jogo nao implementado - Apenas para teste");
        }	   

        UpdateScreen();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {            
            if (userInput.text != null)
            {
                AdminUserInput(userInput.text);
                UpdateScreen();
            }
        }
	}

    private void UpdateScreen()
    {
        if (!deckMannager.IsQueueEmpty())
        {
            currentCard = deckMannager.GetNextCard();
            cardLabel.text = currentCard.PortugueseText;
            userInput.text = "";
        }
        else
        {
            levelManager = new LevelManager();
            levelManager.LoadLevel("EndGameScreen");
        }
    }    


    private void AdminUserInput(string userInput)
    {
        if (userInput.Equals(currentCard.EnglishText))
        {
        	GlobalVariables.correctAnswerAmount++;
            Debug.Log("Voce acertou");
        }
        else
        {
        	GlobalVariables.wrongAnswerAmount++;
            Debug.Log("Voce errou");
        }
    }
}
