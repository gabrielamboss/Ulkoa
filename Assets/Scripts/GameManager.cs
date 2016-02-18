using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public InputField userInput;
    public bool test;
    private Card currentCard;
    private GameDeckMannager deckMannager;
    private LevelManager levelManager = new LevelManager();
    private SRSManager leitnerManager;
    private Deck currentDeck;
    public GameObject cardPrefab;
    private GameObject cardObject;
    public GameObject correctLeavingCardPrefab;
    public GameObject wrongLeavingCardPrefab;
    private static String correctAnswer;
    public GameObject WrongSymbol;
    public GameObject CorrectSymbol;
    public Button goBacktoMenu;    

    // Use this for initialization
    void Start()
    {
        GlobalVariables.WasNotDisplayed = true;
        GlobalVariables.correctAnswerAmount = 0;
        GlobalVariables.wrongAnswerAmount = 0;
        if (test)
        {
            Debug.Log("Test mode");
            currentDeck = MockDeckFabric.DefaultDeck();
            leitnerManager = new LeitnerManager(currentDeck);
            deckMannager = new GameDeckMannager(currentDeck);
        }
        else
        {
            currentDeck = GlobalVariables.GetSelectedDeck();
            Debug.Log("Playng Deck: " + currentDeck.DeckName);
            leitnerManager = new LeitnerManager(currentDeck);
        }

        Debug.Log("Times played: " + currentDeck.TimesPlayed);
        Debug.Log("IsFirtTime: " + currentDeck.IsFirstTime);
        if (GlobalVariables.continueGame)
        {
            UpdateScreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.continueGame)
        {
            goBacktoMenu.enabled = true;
            if (leitnerManager.IsFirst())
            {
                UpdateScreen();
            }
        }
        if (GlobalVariables.continueGame)
        {
            goBacktoMenu.enabled = true;
            FocusInput();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (userInput.text != null && userInput.text != "")
                {
                    //GlobalVariables.UserAnswers.Add(currentCard.ObjectId, userInput.text); Vamo ter que mudar isso
                    AdminUserInput(userInput.text);
                    if (GlobalVariables.continueGame)
                    {
                        UpdateScreen();
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
        saveMatch();
        levelManager.LoadLevel(SceneBook.END_GAME_NAME);
    }

    private void saveMatch()
    {
        //Saving match
        Match match = new Match();
        match.CorrectPoints = GlobalVariables.correctAnswerAmount;
        match.WrongPoints = GlobalVariables.wrongAnswerAmount;
        match.DeckName = GlobalVariables.GetSelectedDeck().DeckName;
        MatchDao matchDao = new MatchDao();
        StartCoroutine(matchDao.saveMatch(match));
    }

    private void saveCards()
    {
        List<Card> sessionCards = leitnerManager.GetSessionCards();
        foreach (Card card in sessionCards)
        {
            Debug.Log("Letiner level da carta " + card.EnglishText + ": " + card.LeitnerLevel);
            //card.SaveAsync();
        }
    }

    private void saveDeck()
    {
        currentDeck.TimesPlayed++;
        StartCoroutine(new PlayerDao().savePlayer(Player.getInstance()));
        Debug.Log("Deck foi salvo com sucesso. Numero de vezes jogos com este Deck: " + currentDeck.TimesPlayed);
    }

    private void doGameLoop()
    {
        currentCard = leitnerManager.GetNextCard();
        correctAnswer = currentCard.EnglishText;
        cardObject = Instantiate(cardPrefab, new Vector3(0, Screen.height / 2 + 120, 0), Quaternion.identity) as GameObject;
        cardObject.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
        cardObject.GetComponentInChildren<Text>().text = currentCard.PortugueseText;
    }

    private void AdminUserInput(string userInput)
    {
        goBacktoMenu.enabled = false;
        int Error = LevenshteinDistance.LevenshteinDistanceFunction(userInput.ToLower(), currentCard.EnglishText.ToLower());
        var ErrorMeasure = (float)(Error * 100 / currentCard.EnglishText.Length);
        Debug.Log("ErrorMeasure: " + ErrorMeasure);
        if (ErrorMeasure == 0)
        {
            HandleCorrectInput();
        }
        else if (ErrorMeasure > 0 && ErrorMeasure <= 20)
        {
            HandleAlmostCorrectInput();
        }
        else
        {
            HandleWrongInput();
        }
    }

    private void HandleAlmostCorrectInput()
    {
        if (GlobalVariables.continueGame)
        {
            GlobalVariables.wrongAnswerAmount++;
            if (currentCard.LeitnerLevel > 1) currentCard.LeitnerLevel--;
            WrongSymbol.GetComponent<Animator>().Play("Playing");
            DestroyImmediate(cardObject);
            exitWrongCard(cardObject);
            Debug.Log("Voce quase acertou");
        }
    }

    private void HandleCorrectInput()
    {
        GlobalVariables.correctAnswerAmount++;
        if (currentCard.LeitnerLevel < 5) currentCard.LeitnerLevel++;
        CorrectSymbol.GetComponent<Animator>().Play("Playing");
        DestroyImmediate(cardObject);
        exitCorrectCard(cardObject);
        Debug.Log("Voce acertou");
    }

    public void HandleWrongInput()
    {
        if (GlobalVariables.continueGame)
        {
            goBacktoMenu.enabled = false;
            GlobalVariables.wrongAnswerAmount++;
            currentCard.LeitnerLevel = 1;
            WrongSymbol.GetComponent<Animator>().Play("Playing");
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
        GlobalVariables.continueGame = false;
        yield return new WaitForSeconds(3);
        GlobalVariables.continueGame = true;
        UpdateScreen();
    }

    IEnumerator WaitToCardGetDownToLeaveWrong()
    {
        GlobalVariables.continueGame = false;
        yield return new WaitForSeconds(4);
        GlobalVariables.continueGame = true;
        UpdateScreen();
    }

    IEnumerator WaitToCardGetDownToLeaveCorrect()
    {
        GlobalVariables.continueGame = false;
        yield return new WaitForSeconds(4);
        GlobalVariables.continueGame = true;
        UpdateScreen();
    }

    public void BackToMainMenu()
    {
        foreach (Card card in leitnerManager.GetSessionCards())
        {
            //card.LeitnerLevel = GlobalVariables.OriginalLevels[card.ObjectId]; Vamo ter que mudar isso
        }
        levelManager.LoadLevel(SceneBook.MAIN_MENU_NAME);
    }

}
