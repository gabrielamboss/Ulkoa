using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardHolder : MonoBehaviour {

    private Card myCard;

    void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(()=>{
            GameObject go = GameObject.Find("Deck Creator");
            go.GetComponent<DeckCreatorController>().OnCardClick(myCard);
        });
    }

    public void SetCard(Card card)
    {
        myCard = card;
    }

    public Card GetCard()
    {        
        return myCard;
    }
    
}
