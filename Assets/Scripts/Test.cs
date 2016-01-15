using Parse;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
       
    // Use this for initialization
    void Start () {
        PreDeckCreator3();
    } 
    
    private void PreDeckCreator3()
    {
        Player2.Init();
        StartCoroutine(PreDeckCreator3_Continue());
    }       

    private IEnumerator PreDeckCreator3_Continue()
    {
        while (!Player2.IsInitialized())
        {
            yield return null;
        }

        List<Deck> deckList = Player2.GetDeckList();        
        foreach (Deck deck in deckList)
        {
            if (deck.DeckName.Equals("deckCreatorTeste"))
            {
                GlobalVariables.SetSelectedDeck(deck);
            }
        }

        Debug.Log("PreDeckCreator3 has finished");
    }

}
