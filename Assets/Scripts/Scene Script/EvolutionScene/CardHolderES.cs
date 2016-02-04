using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardHolderES : MonoBehaviour {

    private Card myCard;
    private string portugeseText;
    private string englishText;

    void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(()=>{
            GameObject go = GameObject.Find("Deck Creator");
            go.GetComponent<EvolutionSceneController>().OnCardClick(gameObject);
        });
    }

    public void setCard(Card card)
    {
        myCard = card;
        setPortugueseText(card.PortugueseText);
        setEnglishText(card.EnglishText);
    }

    public Card getCard()
    {        
        return myCard;
    }
    
    public void setPortugueseText(string port)
    {
        portugeseText = port;
        gameObject.GetComponentInChildren<Text>().text = port;
    }

    public string getPortugueseText()
    {
        return portugeseText;
    }

    public void setEnglishText(string engl)
    {
        englishText = engl;
    }

    public string getEnglishText()
    {
        return englishText;
    }

    public void updateCard()
    {
        myCard.PortugueseText = portugeseText;
        myCard.EnglishText = englishText;
    }

    public int getLeitnerLevel()
    {
        return myCard.LeitnerLevel;
    }

    public int getPreviousLeitnerLevel()
    {
        return GlobalVariables.OriginalLevels[myCard.ObjectId];
    }
    
    public string getUserAnswer()
    {
        return GlobalVariables.UserAnswers[myCard.ObjectId];
    }
}
