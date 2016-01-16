using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeckHolder : MonoBehaviour {

    private Deck myDeck;

    void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(() => {
            GameObject go = GameObject.Find("CollectionMenu");
            go.GetComponent<CollectionMenu>().OnDeckClick(myDeck);
        });
    }

    public void SetDeck(Deck deck)
    {
        myDeck = deck;
    }

    public Deck GetCard()
    {
        return myDeck;
    }
}
