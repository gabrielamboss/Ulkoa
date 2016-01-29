using Parse;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    private Text userName;
    private Text stars;

    public Sprite selectedDeckImage;
    public Sprite normalDeckImage;
    public GameObject deckPrefab;
    private GameObject selectedDeckUI;
    private GameObject collectionParent;

    void Awake()
    {
        collectionParent = GameObject.Find("Content");

        GameObject userNameGO = GameObject.Find("User Name");
        userName = userNameGO.GetComponent<Text>();

        stars = GameObject.Find("Stars").GetComponent<Text>();
        stars.text = Player.getInstance().Currency.ToString();
    }
	
	void Start () {

        Player player = Player.getInstance();        
        userName.text = player.getName();

        IList<Deck> deckList = player.getDeckList();
        foreach (Deck deck in deckList)
        {
            GameObject newDeck = Instantiate(deckPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newDeck.transform.SetParent(collectionParent.transform, false);
            newDeck.GetComponent<DeckHolder>().SetDeck(deck);
            Text text = newDeck.GetComponentInChildren<Text>();
            text.text = deck.DeckName;
        }

        selectedDeckUI = collectionParent.transform.GetChild(0).gameObject;
        selectedDeckUI.GetComponent<Image>().sprite = selectedDeckImage;
    }		    

    public void OnDeckClick(GameObject deckUI)
    {
        selectedDeckUI.GetComponent<Image>().sprite = normalDeckImage;
        deckUI.GetComponent<Image>().sprite = selectedDeckImage;
        selectedDeckUI = deckUI;
        Deck deck = deckUI.GetComponent<DeckHolder>().GetDeck();
        Debug.Log(deck.DeckName);
        GlobalVariables.SetSelectedDeck(deck);
    }       

    public void Play()
    {
        new LevelManager().LoadLevel(SceneBook.GAME_NAME);
    }

    public void EditDeck()
    {
        Deck deck = GlobalVariables.GetSelectedDeck();
        if(deck == null)
        {
            Debug.Log("Usuario nao selecionou deck");
            return;
        }

        if (deck.IsEditable == false)
        {
            Debug.Log("Usuario nao editar esse deck");
            return;
        }
        new LevelManager().LoadLevel(SceneBook.DECK_CREATOR_NAME);
    }

    public void NewDeck()
    {
        Deck newDeck = new Deck();
        newDeck.DeckName = "";
        GlobalVariables.SetSelectedDeck(newDeck);
        new LevelManager().LoadLevel(SceneBook.DECK_CREATOR_NAME);
    }

    public void DeleteDeck()
    {
        Deck selectedDeck = GlobalVariables.GetSelectedDeck();
        if (selectedDeck != null && selectedDeck.IsEditable)
        {            
            foreach (Transform deckUI in collectionParent.transform)
            {                
                Deck deck = deckUI.GetComponent<DeckHolder>().GetDeck();

                if (deck.Equals(selectedDeck))
                {
                    List<Card> cardList = deck.getCardList();

                    deck.DeleteAsync();
                    foreach (Card card in cardList)
                    {
                        card.DeleteAsync();
                    }

                    Destroy(deckUI.gameObject);

                    Player.getInstance().removeDeck(deck);
                }                
            }
        }
        
    }

    public void GoToStore()
    {
        new LevelManager().LoadLevel(SceneBook.STORE_NAME);
    }

    public void Logout()
    {
        Player.setInstance(null);
        Store.clean();
        ParseUser.LogOutAsync();
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }
}
