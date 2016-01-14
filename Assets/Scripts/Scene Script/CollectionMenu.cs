using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CollectionMenu : MonoBehaviour {

    public bool test = false;
    public Object deckPrefab;
    public GameObject collectionParent;

	// Use this for initialization
	void Start () {
        if (test)
        {            
            Player2.Init();
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadDecksIntoCollection(){
		
        IList<Deck> deckList = Player2.GetDeckList();


        foreach(Deck deck in deckList){
			GameObject newDeck = Instantiate(deckPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			newDeck.transform.SetParent(collectionParent.transform, false);
        	Text text = newDeck.GetComponentInChildren<Text>();
        	text.text = deck.DeckName;
        }




	}
}
