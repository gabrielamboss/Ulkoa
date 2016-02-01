using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckCreatorView : MonoBehaviour {

    private DeckCreatorModel model;

    public GameObject cardContainer;

    public InputField deckName;
    public InputField portugueseText;
    public InputField englishText;    

    public void init()
    {
        GameObject deckCreator = GameObject.Find("Deck Creator");
        model = deckCreator.GetComponent<DeckCreatorModel>();

        deckName.text = model.getDeckName();
        updateCardContainer();
        updatePorEnglText();
    }

    public void updateCardContainer()
    {
        while (cardContainer.transform.childCount > 0)
        {            
            Transform child = cardContainer.transform.GetChild(0);
            child.parent = null;
        }

        List<GameObject> cardUIList = model.getCardUIList();

        cardUIList.Sort(delegate (GameObject go1, GameObject go2)
        {
            CardHolder ch1 = go1.GetComponent<CardHolder>();
            CardHolder ch2 = go2.GetComponent<CardHolder>();
            return ch1.getPortugueseText().CompareTo(ch2.getPortugueseText());
        });

        foreach (GameObject cardUI in cardUIList)
        {            
            cardUI.transform.localScale = new Vector3(1, 1, 1);
            cardUI.transform.SetParent(cardContainer.transform, false);
        }
    }

    public void updatePorEnglText()
    {
        GameObject selectedCardUI = model.getSelectedCardUI();
        CardHolder ch = selectedCardUI.GetComponent<CardHolder>();

        portugueseText.text = ch.getPortugueseText();
        englishText.text = ch.getEnglishText();
    }
    
}
