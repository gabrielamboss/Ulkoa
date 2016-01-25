﻿using UnityEngine;
using UnityEngine.UI;
using Parse;
using System;
using System.Collections;

public class Login : MonoBehaviour {

    public GameObject painel;
    private InputField username;
    private InputField password;

    void Start()
    {
        GameObject usernameGO = GameObject.FindGameObjectWithTag("UIUsername");
        GameObject passwordGO = GameObject.FindGameObjectWithTag("UIPassword");
        username = usernameGO.GetComponent<InputField>();
        password = passwordGO.GetComponent<InputField>();
    }

    public void MakeLogin()
    {
        StartCoroutine(LoginLogic());
    }

    private IEnumerator LoginLogic()
    {        
        painel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();

        Exception e = null;
        bool loginSuccessful = false;
        bool wait = true;

        ParseUser.LogInAsync(username.text, password.text).ContinueWith(t =>
        {
            loginSuccessful = !(t.IsFaulted || t.IsCanceled);
            e = t.Exception;
            wait = false;
        });
        while (wait) { yield return null; }

        if (loginSuccessful)
        {
            new LevelManager().LoadLevel(SceneBook.LOADING_NAME);
        }
        else
        {
            Debug.Log("Login fail");
            Debug.Log(e.ToString());
            //Avisar ao usuario o problema?
        }
                
        painel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

    public void NewAcount()
    {
        new LevelManager().LoadLevel(SceneBook.REGISTER_NAME);
    }



}
