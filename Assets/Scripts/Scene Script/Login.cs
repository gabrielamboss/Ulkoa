using PlayFab;
using PlayFab.ClientModels;
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

        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            TitleId = "2071",
            Username = username.text,            
            Password = password.text           
        };

        PlayFabClientAPI.LoginWithPlayFab(request, 
            (result) =>
        {            
            wait = false;
            if (result.NewlyCreated)
            {
                loginSuccessful = false;
                Debug.Log("Merda criamos um novo usuario");
            }
            else
            {
                loginSuccessful = true;
                Debug.Log("Login com sucesso");
            }
        }, 
            (error) =>
        {
            loginSuccessful = false;
            wait = false;
            Debug.Log("Error logging in player");
            Debug.Log(error.ErrorMessage);
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
