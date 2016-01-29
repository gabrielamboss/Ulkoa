using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreDeckUI : MonoBehaviour {

    private StoreDeck myDeck;    

    void Start()
    {
        Button b = gameObject.GetComponent<Button>();        
        b.onClick.AddListener(() => {
            GameObject go = GameObject.Find("Store Manager");
            go.GetComponent<StoreManager>().OnDeckClick(gameObject);
        });
        
    }

    public StoreDeck getStoreDeck()
    {
        return myDeck;
    }

    public void SetStoreDeck(StoreDeck storeDeck)
    {
        myDeck = storeDeck;
        updateUI();
    }
	
    private void updateUI()
    {
        Text[] deckTexts = gameObject.GetComponentsInChildren<Text>();

        deckTexts[0].text = myDeck.DeckName;        

        if (myDeck.IsPremium)
        {
            deckTexts[1].text = "Premium";
        }
        else
        {
			deckTexts[1].text = "✭: " + myDeck.Price.ToString();
        }

    }    

}
