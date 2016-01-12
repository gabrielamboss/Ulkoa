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
            ParseUser.LogInAsync("teste", "teste");
        }

        if (ParseUser.CurrentUser != null)
        {
            Debug.Log("Voce esta logado como: " + ParseUser.CurrentUser.Username);
        }
        else
        {
            Debug.Log("Voce nao esta logado");
        }
	}
		
}
