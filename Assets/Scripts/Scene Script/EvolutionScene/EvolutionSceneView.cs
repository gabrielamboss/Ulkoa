using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EvolutionSceneView : MonoBehaviour {

    private EvolutionSceneModel model;

    public GameObject cardContainer;


    public InputField portugueseText;
    public InputField englishText;

    public Text previousLeitnerLevel;
    public Text actualLeitnerLevel;
    public Text deckName;
    public Text answer;
    public Text sessionNumber;

    public void init()
    {
        GameObject deckCreator = GameObject.Find("Deck Creator");
        model = deckCreator.GetComponent<EvolutionSceneModel>();

        deckName.text = model.getDeckName();
        sessionNumber.text = model.getSessionNumber();
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
            CardHolderES ch1 = go1.GetComponent<CardHolderES>();
            CardHolderES ch2 = go2.GetComponent<CardHolderES>();
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
        CardHolderES ch = selectedCardUI.GetComponent<CardHolderES>();

        portugueseText.text = ch.getPortugueseText();
        englishText.text = ch.getEnglishText();
        actualLeitnerLevel.text = ch.getLeitnerLevel();
        previousLeitnerLevel.text = ch.getPreviousLeitnerLevel();
        answer.text = ch.getUserAnswer();
    }
    
}
