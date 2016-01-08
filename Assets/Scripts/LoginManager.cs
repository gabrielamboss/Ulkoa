using UnityEngine;
using System.Collections;
using Parse;

public class LoginManager{

	public void Logout()
    {
        if (ParseUser.CurrentUser != null)
        {
            ParseUser.LogOutAsync();
        }
    }
}
