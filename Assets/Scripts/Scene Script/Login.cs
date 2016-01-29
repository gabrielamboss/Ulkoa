using Parse;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

    public GameObject errorText;
    public GameObject painel;
    public InputField username;
    public InputField password;

    void Start()
    {        
        errorText.SetActive(false);
    }

    public void MakeLogin()
    {
        StartCoroutine(LoginLogic());
    }

    private IEnumerator LoginLogic()
    {        
        painel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();
        
        bool loginSuccessful = false;
        bool wait = true;

        ParseUser.LogInAsync(username.text, password.text).ContinueWith(t =>
        {
            loginSuccessful = !(t.IsFaulted || t.IsCanceled);
            wait = false;
        });
        while (wait) { yield return null; }

        if (loginSuccessful)       
            new LevelManager().LoadLevel(SceneBook.LOADING_NAME);        
        else        
            errorText.SetActive(true);        
                
        painel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

    public void NewAcount()
    {
        new LevelManager().LoadLevel(SceneBook.REGISTER_NAME);
    }



}
