using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using UnityEngine;

public class Authenticator {

    public delegate void LoginSuccesfullCallBack(LoginResult result);
    public delegate void LoginFailCallBack(PlayFabError error);

    public delegate void RegisterSuccesfullCallBack(RegisterPlayFabUserResult result);
    public delegate void RegisterFailCallBack(PlayFabError error);

    private string username;
    private string password;
    private string email;

    LoginSuccesfullCallBack loginSuccesfull;
    LoginFailCallBack loginFail;

    RegisterSuccesfullCallBack registerSuccesfull;
    RegisterFailCallBack registerFail;

    private bool wait;

    public Authenticator setUserName(string username)
    {
        this.username = username;
        return this;
    }
    public Authenticator setPassword(string password)
    {
        this.password = password;        
        return this;
    }
    public Authenticator setEmail(string email)
    {
        this.email = email;
        return this;
    }
    public Authenticator setLoginSuccesfullCallback(LoginSuccesfullCallBack callback)
    {
        loginSuccesfull = callback;
        return this;
    }
    public Authenticator setLoginFailCallback(LoginFailCallBack callback)
    {
        loginFail = callback;
        return this;
    }
    public Authenticator setRegisterSuccesfullCallback(RegisterSuccesfullCallBack callback)
    {
        registerSuccesfull = callback;
        return this;
    }
    public Authenticator setRegisterFailCallback(RegisterFailCallBack callback)
    {
        registerFail = callback;
        return this;
    }

    public IEnumerator makeLogin()
    {        
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            Password = password
        };

        wait = true;

        PlayFabClientAPI.LoginWithPlayFab(request,
            (result) =>
            {                
                if (result.NewlyCreated)
                {                    
                    Debug.Log("Merda criamos um novo usuario");
                    Debug.Log("O que eu fazer agora ?");
                }
                else
                {                    
                    Debug.Log("Login com sucesso");
                    loginSuccesfull(result);
                }
                wait = false;
            },
            (error) =>
            {                
                Debug.Log("Error logging in player");
                loginFail(error);
                wait = false;
            });

        while (wait)
        { yield return null; }
    }

    public IEnumerator makeRegister()
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            Email = email,
            Password = password,
            RequireBothUsernameAndEmail = true
        };

        wait = true;

        PlayFabClientAPI.RegisterPlayFabUser(request,
            (result) =>
            {
                registerSuccesfull(result);
                wait = false;
            },
            (error) =>
            {
                registerFail(error);
                wait = false;                
            });

        while (wait)
        { yield return null; }
    }
}
