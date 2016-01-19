using UnityEngine;
using System.Collections;
using Parse;

public class LoutMainMenu : MonoBehaviour {

    public void Logout()
    {
        if (ParseUser.CurrentUser != null)
        {
            ParseUser.LogOutAsync();
        }
    }
}
