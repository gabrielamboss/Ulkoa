using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class DeckCreator : MonoBehaviour {

    private InputField deckName = null;
    private InputField english;
    private InputField portuguese;
    private int cardsCont;
    private Text totalCards;

    private Deck deck;
    private IList<Card> cardList;

    void Awake()
    {
        ParseObject.RegisterSubclass<Card>();
        ParseObject.RegisterSubclass<Deck>();        
    }

    void Start()
    {
        GameObject aux = GameObject.Find("Deck Name");
        deckName = aux.GetComponent<InputField>();

        aux = GameObject.Find("English Field");
        english = aux.GetComponent<InputField>();

        aux = GameObject.Find("Portuguese Field");
        portuguese = aux.GetComponent<InputField>();

        aux = GameObject.Find("Total Cards");
        totalCards = aux.GetComponent<Text>();

        totalCards.text = "0";
        cardsCont = 0;

        deck = new Deck();
        cardList = new List<Card>();        
    }

    public void AddCard()
    {
        if (portuguese.text != "" && english.text != "")
        {            
            Card card = new Card();
            card.PortugueseText = portuguese.text;
            card.EnglishText = english.text;
            cardList.Add(card);

            totalCards.text = (++cardsCont).ToString();

            //portuguese.text = "";
            //english.text = "";
        }
        else
        {
            Debug.Log("User cant create a card without the english text or the portugese text");
			popUpContent = "Todos os campos devem estar preenchidos!";
            popUpIsDisplayed = true;
        }
    }

    public void SaveDeck()
    {
        if (deckName.text != "")
        {
            //Save deck            
            deck.DeckName = deckName.text;
            deck.UserId = ParseUser.CurrentUser.ObjectId;
            deck.IsEditable = false;
            wait = true;
            deck.SaveAsync().ContinueWith(t=>
            {
                Debug.Log("Deck Saved");
                wait = false;
            });
            StartCoroutine(SaveCards());       
        }
        else
        {
            Debug.Log("User cant save a deck without a name");
			popUpContent = "O deck deve ter um nome!";
			popUpIsDisplayed = true;
        }
    }

	public void Exit()
    {
        new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);
    }


    private IEnumerator SaveCards()
    {
        while (wait)
        {
            yield return null;
        }

        //Save cards
        foreach (Card card in cardList)
        {
            card.UserId = ParseUser.CurrentUser.ObjectId;
            card.DeckId = deck.ObjectId;
            card.SaveAsync();
            //deck.addCard(card);
        }

        new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);
    }

    void OnGUI(){
    	Rect window = new Rect ((Screen.width/2)-150,(Screen.height/2)-30,300,20);

    	if(popUpIsDisplayed){
			GUILayout.Window(0, window, PopUp, popUpContent);
    	}
    }


    void PopUp(int windowID){
		if(GUILayout.Button("Fechar"))
			popUpIsDisplayed = false;
    }

    private bool wait;
    private bool popUpIsDisplayed = false;
    private string popUpContent;
}
