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

    public void Login()//, string password)
    {
        GameObject usernameGO = GameObject.FindGameObjectWithTag("UIUsername");
        GameObject passwordGO = GameObject.FindGameObjectWithTag("UIPassword");
        InputField username = usernameGO.GetComponent<InputField>();
        InputField password = passwordGO.GetComponent<InputField>();
        
        bool loginSuccessful = false;
        wait = true;
        ParseUser.LogInAsync(username.text, password.text).ContinueWith(t=>
        {
            wait = false;            
            loginSuccessful = !(t.IsFaulted || t.IsCanceled);
            Debug.Log("Loggin Finish1");
        });
        WaitLogginFinish();
        Debug.Log("Loggin Finish2");
        if (loginSuccessful)
        {
            new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);
        }
        else
        {
            Debug.Log("Login fail");
        }
    }

    private IEnumerator WaitLogginFinish()
    {
        while (wait)
        {
            yield return null;
        }
    }

    private bool wait;
}
