using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CollectionMenu : MonoBehaviour {

    private Text userName;

    public GameObject deckPrefab;
    private GameObject collectionParent;

    void Awake()
    {
        collectionParent = GameObject.Find("Content");

        GameObject userNameGO = GameObject.Find("User Name");
        userName = userNameGO.GetComponent<Text>();
    }
	
	void Start () {

        userName.text = Player2.GetPlayerName();

        IList<Deck> deckList = Player2.GetDeckList();

        foreach (Deck deck in deckList)
        {
            GameObject newDeck = Instantiate(deckPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newDeck.transform.SetParent(collectionParent.transform, false);
            newDeck.GetComponent<DeckHolder>().SetDeck(deck);
            Text text = newDeck.GetComponentInChildren<Text>();
            text.text = deck.DeckName;
        }
        
    }		    

    public void OnDeckClick(Deck deck)
    {
        Debug.Log(deck.DeckName);
        GlobalVariables.SetSelectedDeck(deck);
    }       

    public void Play()
    {

    }

    public void EditDeck()
    {
        new LevelManager().LoadLevel("DeckCreator3");
    }

    public void NewDeck()
    {

    }

    public void DeleteDeck()
    {

    }

    public void Logout()
    {

    }
}
