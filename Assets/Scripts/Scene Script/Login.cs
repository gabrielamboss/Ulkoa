using UnityEngine;
using UnityEngine.UI;
using Parse;
using System.Collections;

public class Login : MonoBehaviour {

    private InputField username;
    private InputField password;

    private bool loginSuccessful;
    private bool wait;

    void Start()
    {
        GameObject usernameGO = GameObject.FindGameObjectWithTag("UIUsername");
        GameObject passwordGO = GameObject.FindGameObjectWithTag("UIPassword");
        username = usernameGO.GetComponent<InputField>();
        password = passwordGO.GetComponent<InputField>();
    }

    public void MakeLogin()
    {        
        loginSuccessful = false;
        wait = true;

        ParseUser.LogInAsync(username.text, password.text).ContinueWith(t =>
        {            
            loginSuccessful = !(t.IsFaulted || t.IsCanceled);
            Debug.Log("Loggin Finish1");
            wait = false;
        });


        StartCoroutine(WaitLoginFinish());
    }

    private IEnumerator WaitLoginFinish()
    {
        while (wait)
        {
            yield return null;
        }
        Debug.Log("Loggin Finish2");

        if (loginSuccessful)
        {
            new LevelManager().LoadLevel(SceneBook.LOADING_NAME);
        }
        else
        {
            Debug.Log("Login fail");
        }
    }

    public void NewAcount()
    {
        new LevelManager().LoadLevel(SceneBook.REGISTER_NAME);
    }

    
}
