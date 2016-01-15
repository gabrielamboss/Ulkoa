using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class DeckCreatorView : MonoBehaviour {

    private DeckCreatorModel model = null;    
    public GameObject cardPrefab;
    private GameObject collectionParent = null;
    private InputField portugueseText = null;
    private InputField englishText = null;

    void Awake()
    {
        GameObject deckCreator = GameObject.Find("Deck Creator");
        model = deckCreator.GetComponent<DeckCreatorModel>();        

        GameObject portObj = GameObject.Find("Portuguese Field");
        portugueseText = portObj.GetComponent<InputField>();       

        GameObject englObj = GameObject.Find("English Field");
        englishText = englObj.GetComponent<InputField>();

        collectionParent = GameObject.Find("Content");        
    }
	
	void Start () {       
        
    }
	
	public void UpdateScreen()
    {
        CleanScreen();

        List<Card> cardList = model.GetCardList();
        foreach (Card card in cardList) {
            Debug.Log(card.PortugueseText + " " + card.EnglishText);
            AddCardToScrollField(card);
        }

        Card newCard = model.GetNewCard();
        Debug.Log(newCard.PortugueseText + " " + newCard.EnglishText);
        AddCardToScrollField(newCard);

        Card selectedCard = model.GetSelectedCard();
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
            Destroy(child.gameObject);
        }                
    }

    private void AddCardToScrollField(Card card)
    {
        GameObject cardObject = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        cardObject.transform.SetParent(collectionParent.transform, false);
        cardObject.GetComponent<CardHolder>().SetCard(card);
        Text text = cardObject.GetComponentInChildren<Text>();
        text.text = card.PortugueseText;
    }
}
