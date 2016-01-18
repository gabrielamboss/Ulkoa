using Parse;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Text cardLabel;
    public InputField userInput;
    public bool test;
    private Card currentCard;
    private GameDeckMannager deckMannager;
    private LevelManager levelManager;
    private SRSManager leitnerManager;
    private Deck currentDeck;
    private GameView gameView;
    public GameObject cardPrefab;
    private GameObject cardObject;
    public GameObject correctLeavingCardPrefab;
    public GameObject wrongLeavingCardPrefab;

    void Awake()
    {
        ParseObject.RegisterSubclass<Card>();
        ParseObject.RegisterSubclass<Deck>();
        ParseObject.RegisterSubclass<CardCollection>();
    }

    // Use this for initialization
    void Start()
    {
        GlobalVariables.correctAnswerAmount = 0;
        GlobalVariables.wrongAnswerAmount = 0;
        if (test)
        {
            Debug.Log("Test");
            currentDeck = MockDeckFabric.DefaultDeck();
            leitnerManager = new LeitnerManager(currentDeck);
            deckMannager = new GameDeckMannager(currentDeck);
        }
        else
        {
            Debug.Log("Jogo nao implementado - Apenas para teste");
        }

        setCurrentDeckToView(currentDeck);
        UpdateScreen();
    }

    private void setCurrentDeckToView(Deck currentDeck)
    {
        GameObject gameView = GameObject.Find("GameView");

    }

    // Update is called once per frame
    void Update()
    {
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
        if (!leitnerManager.IsCurrentDeckEmpty())
        {
            doGameLoop();
        }
        else
        {
            saveAndFinalize();
        }
    }

    private void saveAndFinalize()
    {
        Debug.Log("Jogo finalizado, salvando dados e mudando de cena:");
        saveDeck();
        saveCards();
        levelManager = new LevelManager();
        levelManager.LoadLevel("EndGameScreen");
    }

    private void saveCards()
    {
        List<Card> sessionCards = leitnerManager.GetSessionCards();
        foreach (Card card in sessionCards)
        {
            Debug.Log(card.LeitnerLevel);
            card.SaveAsync();
        }
    }

    private void saveDeck()
    {
        currentDeck.TimesPlayed++;
        currentDeck.SaveAsync();
        Debug.Log("Deck foi salvo com sucesso. Numero de vezes jogos com este Deck: " + currentDeck.TimesPlayed);
    }

    private void doGameLoop()
    {
        currentCard = leitnerManager.GetNextCard();
        Debug.Log("Instanciando");
        cardObject = Instantiate(cardPrefab, new Vector3(0, Screen.height/2 + 120, 0), Quaternion.identity) as GameObject;
        cardObject.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
        cardObject.GetComponentInChildren<Text>().text = currentCard.PortugueseText;
    }

    private void AdminUserInput(string userInput)
    {
        if (userInput.Equals(currentCard.EnglishText))
        {
            GlobalVariables.correctAnswerAmount++;
            if (currentCard.LeitnerLevel < 5) currentCard.LeitnerLevel++;
            Debug.Log(cardObject.GetComponentInChildren<Text>().text);
            DestroyImmediate(cardObject);
            cardObject = Instantiate(correctLeavingCardPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            cardObject.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
            cardObject.GetComponentInChildren<Text>().text = currentCard.PortugueseText;
            Debug.Log("Voce acertou");
        }
        else
        {
            GlobalVariables.wrongAnswerAmount++;
            currentCard.LeitnerLevel = 1;
            Debug.Log(cardObject.GetComponentInChildren<Text>().text);
            DestroyImmediate(cardObject);
            cardObject = Instantiate(wrongLeavingCardPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            cardObject.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
            cardObject.GetComponentInChildren<Text>().text = currentCard.PortugueseText;
            Debug.Log("Voce errou");
        }
    }
}
