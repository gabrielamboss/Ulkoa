using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class DeckCreatorView : MonoBehaviour {

    private DeckCreatorModel model = null;      
    private GameObject collectionParent = null;
    private InputField deckName = null;
    private InputField portugueseText = null;
    private InputField englishText = null;

    void Awake()
    {
        GameObject deckCreator = GameObject.Find("Deck Creator");
        model = deckCreator.GetComponent<DeckCreatorModel>();

        GameObject deckNameObj = GameObject.Find("Deck Name");
        deckName = deckNameObj.GetComponent<InputField>();

        GameObject portObj = GameObject.Find("Portuguese Field");
        portugueseText = portObj.GetComponent<InputField>();       

        GameObject englObj = GameObject.Find("English Field");
        englishText = englObj.GetComponent<InputField>();

        collectionParent = GameObject.Find("Content");        
    }
	
	void Start () {       
        
    }

    public void SetDeckNameScreen() {
        deckName.text = model.GetDeckName();
    }

	public void UpdateScreen()
    {
        CleanScreen();        

        List<GameObject> cardList = model.GetCardList();
        foreach (GameObject cardUI in cardList) {
            //Debug.Log(card.PortugueseText + " " + card.EnglishText);
            //AddCardToScrollField(card);
            cardUI.transform.localScale = new Vector3(1,1,1);
            cardUI.transform.SetParent(collectionParent.transform, false);
        }        

        GameObject selectedCardUI = model.GetSelectedCard();
        Card selectedCard = selectedCardUI.GetComponent<CardHolder>().GetCard();
        Debug.Log(selectedCard.PortugueseText + " " + selectedCard.EnglishText);
        portugueseText.text = selectedCard.PortugueseText;
        englishText.text = selectedCard.EnglishText;

        
    }

    private void CleanScreen()
    {
        while (collectionParent.transform.childCount > 0)
        {
            Debug.Log(collectionParent.transform.childCount);
            Transform child = collectionParent.transform.GetChild(0);
            child.parent = null;            
        }                
    }

    /*
    private void AddCardToScrollField(Card card)
    {
        GameObject cardObject = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        cardObject.transform.SetParent(collectionParent.transform, false);
        cardObject.GetComponent<CardHolder>().SetCard(card);
        Text text = cardObject.GetComponentInChildren<Text>();
        text.text = card.PortugueseText;
    }
    */

}
