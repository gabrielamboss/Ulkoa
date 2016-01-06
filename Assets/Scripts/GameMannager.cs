using Parse;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameMannager : MonoBehaviour {

    public Text cardLabel;
    public Text userInput;
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
            cardLabel.text = currentCard.FrontText;
            userInput.text = "test";
        }
        else
        {
            levelManager = new LevelManager();
            levelManager.LoadLevel("EndGameScreen");
        }
    }    


    private void AdminUserInput(string userInput)
    {
        if (userInput.Equals(currentCard.BackText))
        {
            Debug.Log("Voce acertou");
        }
        else
        {
            Debug.Log("Voce errou");
        }
    }
}
