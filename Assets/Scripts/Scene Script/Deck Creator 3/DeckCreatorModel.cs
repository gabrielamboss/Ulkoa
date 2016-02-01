using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckCreatorModel : MonoBehaviour {
    public GameObject cardUI;
    
    private Deck deck;
    
    private List<GameObject> cardUIList = new List<GameObject>();
    private List<Card> cardsToDelete = new List<Card>();
    private GameObject selectedCardUI = null;

    public Sprite selectedCardImage;
    public Sprite normalCardImage;        

	public void init(Deck deck)
    {
        this.deck = deck;
        List<Card> cardList = deck.getCardList();
        foreach (Card card in cardList)
        {
            cardUIList.Add(createUICard(card));
        }
                
        cardUIList.Sort(delegate (GameObject go1, GameObject go2)
        {
            CardHolder ch1 = go1.GetComponent<CardHolder>();
            CardHolder ch2 = go2.GetComponent<CardHolder>();
            return ch1.getPortugueseText().CompareTo(ch2.getPortugueseText());
        });

        selectedCardUI = cardUIList[0];
        setSelectedCardUI(selectedCardUI);
    }

    public List<GameObject> getCardUIList()
    {
        return cardUIList;
    }
    
    public string getDeckName()
    {
        return deck.DeckName;
    }    

    public GameObject getSelectedCardUI()
    {
        return selectedCardUI;
    }

    public void setSelectedCardUI(GameObject cardUI)
    {        
        selectedCardUI.GetComponent<Image>().sprite = normalCardImage;
        selectedCardUI = cardUI;
        selectedCardUI.GetComponent<Image>().sprite = selectedCardImage;        
    }   

    public GameObject addCard(Card card)
    {
        deck.addCard(card);
        GameObject cardUI = createUICard(card);
        cardUIList.Add(cardUI);
        return cardUI;
    }   

    public void deleteSelectedCard()
    {
        cardUIList.Remove(selectedCardUI);        
        cardsToDelete.Add(selectedCardUI.GetComponent<CardHolder>().getCard());        
        Destroy(selectedCardUI);
    }

    private GameObject createUICard(Card card) {
        GameObject cardObject = Instantiate(cardUI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        cardObject.GetComponent<CardHolder>().setCard(card);        
        return cardObject;
    }

    public Deck getDeck()
    {
        return deck;
    }

    public List<Card> getCardsToDelete()
    {
        return cardsToDelete;
    }

    public void cleanGarbage()
    {
        cardsToDelete.Clear();
    }
    
}
