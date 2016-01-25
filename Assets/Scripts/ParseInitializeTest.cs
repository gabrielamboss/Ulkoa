using Parse;
using UnityEngine;
using System.Collections;

public class ParseInitializeTest : MonoBehaviour {
    
    public bool logOut = false;
    public bool logIn = false;

	// Use this for initialization
	void Start () {        
        if (logOut)
        {
            if (ParseUser.CurrentUser != null)
            {
                ParseUser.LogOutAsync();
            }            
        }
        
        if (logIn)
        {
            ParseUser.LogInAsync("novouser2", "novouser2");
        }

        if (ParseUser.CurrentUser != null)
        {
            Debug.Log("Voce esta logado como: " + ParseUser.CurrentUser.Username);
        }
        else
        {
            Debug.Log("Voce nao esta logado");
        }

        Debug.Log("Rgistrando Deck, Card, Player, StoreDeck, StoreCard");
        ParseObject.RegisterSubclass<StoreCard>();
        ParseObject.RegisterSubclass<StoreDeck>();
        ParseObject.RegisterSubclass<Deck>();
        ParseObject.RegisterSubclass<Card>();
        ParseObject.RegisterSubclass<Player>();
    }
		
}
