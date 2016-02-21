using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {
    
    public GameObject painel;
    public InputField username;
    public InputField password;
    public Text userError;
    public Text passwordError;
    public Text errorText;

    void Start()
    {
        username.Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            MakeLogin();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.isFocused)
                password.Select();
            else
                username.Select();
        }
    }

    public void MakeLogin()
    {
        bool inputIsCorrect = true;
        if (username.text.Equals(""))
        {
            userError.text = "Campo usuario em branco";
            inputIsCorrect = false;
        }

        if (password.text.Equals(""))
        {
            passwordError.text = "Campo senha em branco";
            inputIsCorrect = false;
        }

        if (inputIsCorrect)
            StartCoroutine(LoginLogic());
    }

    private IEnumerator LoginLogic()
    {        
        painel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();

        Authenticator authenticator = new Authenticator()
                                            .setUserName(username.text)
                                            .setPassword(password.text)
                                            .setLoginSuccesfullCallback(loginSuccesfull)
                                            .setLoginFailCallback(loginFail);

        yield return authenticator.makeLogin();    
            
        painel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

    private void loginSuccesfull(LoginResult result)
    {
        new LevelManager().LoadLevel(SceneBook.LOADING_NAME);
    }

    private void loginFail(PlayFabError error)
    {
        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidParams:
                errorText.text = "Parametros invalidos (Cod. 1000)";
                break;
            case PlayFabErrorCode.InvalidTitleId:
                errorText.text = "TitleId invalido (Cod.1004)";
                break;
            case PlayFabErrorCode.AccountNotFound:
                errorText.text = "Conta nao encontrada (Cod. 1001)";
                break;
            case PlayFabErrorCode.AccountBanned:
                errorText.text = "Erro de conta (Cod. 1002)";
                break;
            case PlayFabErrorCode.InvalidUsernameOrPassword:
                errorText.text = "Combinação de usuario e senha invalidos";
                break;
            default:
                break;
        }        
    }

    public void NewAcount()
    {
        new LevelManager().LoadLevel(SceneBook.REGISTER_NAME);
    }



}
