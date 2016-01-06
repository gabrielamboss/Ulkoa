using Parse;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardInputManager : MonoBehaviour {

	public InputField inputField;
	public Button flipBtn;
	public Button finishCard;
	public Button finishDeck;

	private Animator animator;
	private Text inputText;
	private Text whereInCard;
	private Text totalCardsNumber;
	private Image quickLoading;
	private Card currCard;
	private CardCollection currCollection;
	private IList<string> currCardIds = new List<string>();
	private bool loadedCollection = false;
	private bool submittedCard = false;
	private bool submittedDeck = false;

	void Awake() {
		ParseObject.RegisterSubclass<Card>();
		ParseObject.RegisterSubclass<Deck>();
		ParseObject.RegisterSubclass<CardCollection>();
	}

	void Start() {
		//Initialize vars
		inputText = inputField.textComponent;
		animator = transform.GetComponent<Animator>();
		whereInCard = transform.Find("WhereInCard").GetComponent<Text>();
		totalCardsNumber = transform.Find("TotalCardsNumber").GetComponent<Text>();
		quickLoading = transform.Find("QuickLoading").GetComponent<Image>();
		InitNewCard();

		//Initialize Listeners
		//Buttons
		//In order to use 'onEndEdit': 'inputField.onEndEdit.AddListener((value) => SubmitName(value));'
		finishDeck.interactable = false;
		finishCard.interactable = false;
		flipBtn.onClick.AddListener(() => SubmitText(inputText.text));
		finishCard.onClick.AddListener(() => SubmitCard(currCard));
		finishDeck.onClick.AddListener(() => SubmitDeck(currCardIds));

		//Initialize current collection
		var query = new ParseQuery<CardCollection>().WhereEqualTo("objectId", (ParseUser.CurrentUser)["collectionId"]);
		query.FindAsync().ContinueWith(t => {
    		IEnumerable<CardCollection> result = t.Result;
			foreach (CardCollection element in result) {
            	currCollection = element;
            	break;
        	}
        	loadedCollection = true;
		});
		StartCoroutine(WaitLoadedCollection());
	}

	IEnumerator WaitLoadedCollection() {
		while(!loadedCollection) {
			yield return null;
		}
		quickLoading.gameObject.SetActive(false);
	}

	IEnumerator WaitSavedDeck() {
		while(!submittedDeck) {
			yield return null;
		}
		quickLoading.gameObject.SetActive(false);
		LevelManager lvlmng = gameObject.AddComponent<LevelManager>();
		lvlmng.LoadLevel("MainMenu");
	}

	IEnumerator WaitForCardMade() {
		while(currCard.FrontText==null || currCard.BackText==null) {
			yield return null;
		}
		finishCard.interactable = true;
	}

	IEnumerator WaitToInitNewCard() {
		while(!submittedCard) {
			yield return null;
		}
		InitNewCard();
	}

	public void InitNewCard() {
		currCard = new Card();
		submittedCard = false;
		totalCardsNumber.text = currCardIds.Count.ToString();
		if(currCardIds.Count > 0) {
			finishDeck.interactable = true;
		}
		StartCoroutine(WaitForCardMade());
	}

	public void SubmitText(string name) {
		Debug.Log("Submitted name was: " + name.Replace("\n", " "));

		if(whereInCard.text == "Front") {
			currCard.FrontText = inputText.text;
			whereInCard.text = "Back";
		} else if(whereInCard.text == "Back") {
			currCard.BackText = inputText.text;
			whereInCard.text = "Front";
		}
		animator.SetTrigger("Flip");
	}

	public void SubmitCard(Card currCard) {
		finishCard.interactable = false;
		inputText.text = "";
		currCard.SaveAsync().ContinueWith(t => {
			currCardIds.Add(currCard.ObjectId);
			IList<string> tempCollection = currCollection.CardIds;
			tempCollection.Add(currCard.ObjectId);
			currCollection.CardIds = tempCollection;
			currCollection.SaveAsync().ContinueWith(t2 => {
				submittedCard = true;
			});
		});
		StartCoroutine(WaitToInitNewCard());
	}

	public void SubmitDeck(IList<string> currCardIds) {
		quickLoading.gameObject.SetActive(true);
		Deck newDeck = new Deck();
		newDeck.DeckName = "Meu pauzinho";
		newDeck.Description = "Como estão?";
		newDeck.CardIds = currCardIds;
		ParseUser currUser = ParseUser.CurrentUser;
		newDeck.SaveAsync().ContinueWith(t => {
			IList<string> tempList = currUser.Get<IList<string>>("decksIds") as IList<string>;
			tempList.Add(newDeck.ObjectId);
			currUser["decksIds"] = tempList;
			currUser.SaveAsync().ContinueWith(t2 => {
				submittedDeck = true;
			});
		});
		StartCoroutine(WaitSavedDeck());
	}



	//**Outer Methods**
	//This method is called from 'Idle' animation
	public void CleanInput() {
		inputField.text = "";
		if(whereInCard.text=="Front" && currCard.FrontText!=null) {
			inputField.text = currCard.FrontText;
		} else if(whereInCard.text=="Back" && currCard.BackText!=null) {
			inputField.text = currCard.BackText;
		}
	}

}