using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeckHolder : MonoBehaviour {

    private Deck myDeck;

    void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(() => {
            GameObject go = GameObject.Find("MainMenu");
            go.GetComponent<MainMenu>().OnDeckClick(myDeck);
        });
    }

    public void SetDeck(Deck deck)
    {
        myDeck = deck;
    }

    public Deck GetDeck()
    {
        return myDeck;
    }
}
