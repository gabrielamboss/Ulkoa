using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    public Text userName;
    public Text stars;
    public Text errorMsg;

    public Sprite selectedDeckImage;
    public Sprite normalDeckImage;

    public GameObject deckPrefab;
    public GameObject deckContainer;
    public GameObject panel;    

    private GameObject selectedDeckUI;    
    	
	void Start () {

        Player player = Player.getInstance();        
        userName.text = player.Username;
        stars.text = player.Currency.ToString();

        buildDeckContainer();
    }		    

    private void buildDeckContainer()
    {        
        Player player = Player.getInstance();

        //Clean deck container
        while (deckContainer.transform.childCount > 0)
        {           
            Transform child = deckContainer.transform.GetChild(0);
            child.parent = null;
            Destroy(child.gameObject);
        }
        
        List<Deck> deckList = player.DeckList.getList();
        deckList.Sort(delegate(Deck d1, Deck d2)
        {
            return d1.DeckName.CompareTo(d2.DeckName);//Compare alfanumericamente
        });

        foreach (Deck deck in deckList)
        {
            GameObject newDeck = Instantiate(deckPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newDeck.transform.SetParent(deckContainer.transform, false);
            newDeck.GetComponent<DeckHolder>().SetDeck(deck);
            Text text = newDeck.GetComponentInChildren<Text>();
            text.text = deck.DeckName;
        }
        
        selectedDeckUI = deckContainer.transform.GetChild(0).gameObject;        
        OnDeckClick(selectedDeckUI);
        
    }

    public void OnDeckClick(GameObject deckUI)
    {
        selectedDeckUI.GetComponent<Image>().sprite = normalDeckImage;
        deckUI.GetComponent<Image>().sprite = selectedDeckImage;
        selectedDeckUI = deckUI;
        Deck deck = deckUI.GetComponent<DeckHolder>().GetDeck();        
        GlobalVariables.SetSelectedDeck(deck);
    }       

    public void Play()
    {
        new LevelManager().LoadLevel(SceneBook.GAME_NAME);
    }

    public void EditDeck()
    {       
        new LevelManager().LoadLevel(SceneBook.DECK_CREATOR_NAME);
    }

    public void NewDeck()
    {
        if (Player.getInstance().IsPremium)
        {
            Deck newDeck = new Deck();
            GlobalVariables.SetSelectedDeck(newDeck);
            new LevelManager().LoadLevel(SceneBook.DECK_CREATOR_NAME);
        }
        else
        {
            errorMsg.text = "Somente usuarios da conta premium podem criar seus proprios decks";
        }        
    }

    public void DeleteDeck()
    {
        Deck selectedDeck = selectedDeckUI.GetComponent<DeckHolder>().GetDeck();
        if (selectedDeck.IsEditable)
        {
            StartCoroutine(deleteLogic(selectedDeck));
        }
        else
        {
            errorMsg.text = "Voce so pode deletar decks criados com a conta premium";
        }
    }

    private IEnumerator deleteLogic(Deck deck)
    {
        panel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();

        Destroy(selectedDeckUI);        

        Player player = Player.getInstance();
        player.removeDeck(deck);

        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.savePlayer(player);

        buildDeckContainer();

        panel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

    public void BePremium()
    {
        Player player = Player.getInstance();
        if (player.IsPremium)
        {
            errorMsg.text = "Você ja é premium, parabens!!";
            return;
        }
        
        StartCoroutine(PremiumLogic());               
    }
    
    private IEnumerator PremiumLogic()
    {
        panel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();

        Player player = Player.getInstance();
        player.IsPremium = true;

        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.savePlayer(player);

        panel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();

        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }

    public void GoToStore()
    {
        new LevelManager().LoadLevel(SceneBook.STORE_NAME);
    }

    public void Logout()
    {        
        Store.clean();        
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }
}
