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
    public GameObject cardPrefab;
    private GameObject cardObject;
    public GameObject correctLeavingCardPrefab;
    public GameObject wrongLeavingCardPrefab;
    private static String correctAnswer;
    private bool continueGame = true;

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
            currentDeck = GlobalVariables.GetSelectedDeck();
            Debug.Log(currentDeck.DeckName);
            leitnerManager = new LeitnerManager(currentDeck);
        }
        
        UpdateScreen();
    }


    // Update is called once per frame
    void Update()
    {
        if (continueGame)
        {
            FocusInput();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (userInput.text != null)
                {
                    AdminUserInput(userInput.text);
                    if (continueGame)
                    {
                        UpdateScreen();
                        Debug.Log("Here");
                    }
                }
            }
        }
    }

    private void UpdateScreen()
    {
        if (!leitnerManager.IsCurrentDeckEmpty())
        {
            doGameLoop();
            ClearAndFocusInput();
        }
        else
        {
            saveAndFinalize();
        }
    }

    private void ClearAndFocusInput()
    {
        userInput.text = "";
        FocusInput();
    }

    private void FocusInput()
    {
        userInput.Select();
        userInput.ActivateInputField();
    }

    private void saveAndFinalize()
    {
        Debug.Log("Jogo finalizado, salvando dados e mudando de cena:");
        saveDeck();
        saveCards();
        levelManager = new LevelManager();
        levelManager.LoadLevel(SceneBook.END_GAME_NAME);
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
        correctAnswer = currentCard.EnglishText;
        Debug.Log("Instanciando");
        cardObject = Instantiate(cardPrefab, new Vector3(0, Screen.height / 2 + 120, 0), Quaternion.identity) as GameObject;
        cardObject.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
        cardObject.GetComponentInChildren<Text>().text = currentCard.PortugueseText;
    }

    private void AdminUserInput(string userInput)
    {
        if (userInput.ToLower().Equals(currentCard.EnglishText.ToLower()))
        {
            GlobalVariables.correctAnswerAmount++;
            if (currentCard.LeitnerLevel < 5) currentCard.LeitnerLevel++;
            Debug.Log(cardObject.GetComponentInChildren<Text>().text);
            DestroyImmediate(cardObject);
            exitCorrectCard(cardObject);
            Debug.Log("Voce acertou");
        }
        else
        {
            GlobalVariables.wrongAnswerAmount++;
            currentCard.LeitnerLevel = 1;
            Debug.Log(cardObject.GetComponentInChildren<Text>().text);
            DestroyImmediate(cardObject);
            exitWrongCard(cardObject);
            Debug.Log("Voce errou");
        }
    }

    private void exitWrongCard(GameObject cardObject)
    {
        cardObject = Instantiate(wrongLeavingCardPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        cardObject.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
        cardObject.GetComponentInChildren<Text>().text = currentCard.PortugueseText;
        if (leitnerManager.IsCurrentDeckEmpty())
        {
            StartCoroutine("WaitToCardGetDownToLeaveWrong");
        }
        else
        {
            StartCoroutine("WaitToCardGetDown");
        }
}

    private void exitCorrectCard(GameObject cardObject)
    {
        cardObject = Instantiate(correctLeavingCardPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        cardObject.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
        cardObject.GetComponentInChildren<Text>().text = currentCard.PortugueseText;
        cardObject.GetComponent<AudioPlayer>().playCorrectAudio();
        if (leitnerManager.IsCurrentDeckEmpty())
        {
            StartCoroutine("WaitToCardGetDownToLeaveCorrect");
        }

    }

    public static String GetCorrectAnswer()
    {
        return correctAnswer;
    }

    IEnumerator WaitToCardGetDown()
    {
        continueGame = false;
        Debug.Log(continueGame);
        yield return new WaitForSeconds(3);
        continueGame = true;
        Debug.Log(continueGame);
        UpdateScreen();
    }

    IEnumerator WaitToCardGetDownToLeaveWrong()
    {
        continueGame = false;
        Debug.Log(continueGame);
        yield return new WaitForSeconds(4);
        continueGame = true;
        Debug.Log(continueGame);
        UpdateScreen();
    }

    IEnumerator WaitToCardGetDownToLeaveCorrect()
    {
        continueGame = false;
        Debug.Log(continueGame);
        yield return new WaitForSeconds(4);
        continueGame = true;
        Debug.Log(continueGame);
        UpdateScreen();
    }
}
