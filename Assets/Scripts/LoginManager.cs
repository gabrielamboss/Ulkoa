using UnityEngine;
using System.Collections;
using Parse;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour{    

    public bool IsPlayerLoggedIn()
    {
        ParseUser user = ParseUser.CurrentUser;

        return user != null;
    }

	public void Logout()
    {
        if (ParseUser.CurrentUser != null)
        {
            ParseUser.LogOutAsync();
        }
    }

    
}
