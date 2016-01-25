using Parse;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

public class Register : MonoBehaviour {

    private InputField username = null;
    private InputField password = null;
    private InputField email = null;

    void Start()
    {
        GameObject usernameGO = GameObject.Find("Username Field");
        GameObject passwordGO = GameObject.Find("Password Field");
        GameObject emailGO = GameObject.Find("Email Field");

        username = usernameGO.GetComponent<InputField>();
        password = passwordGO.GetComponent<InputField>();
        email = emailGO.GetComponent<InputField>();
    }

    public void Save()
    {
        StartCoroutine(SaveLogic());
    }

    private IEnumerator SaveLogic()
    {
        //Start load pop, blanck page, painel...

        bool saveSuccessful = false;
        bool wait = false;
        Exception e;

        //Tentar criar novo usuario
        ParseUser user = new ParseUser();

        user.Username = username.text;
        user.Password = password.text;
        user.Email = email.text;

        wait = true;
        user.SignUpAsync().ContinueWith(t =>
        {
            saveSuccessful = !(t.IsCanceled || t.IsFaulted);
            e = t.Exception;
            wait = false;
        });
        while (wait) { yield return null; }

        if (saveSuccessful)
        {
            //Se conseguimos criar um novo usuario crie para ele um player
            //com as informacoes necessarias do jogador
            Player player = new Player();
            player.UserId = ParseUser.CurrentUser.ObjectId;
            player.Currency = 0;
            player.IsPremium = false;
            player.StoreDeckNameList = new List<string>();
            wait = true;
            player.SaveAsync().ContinueWith(t => { wait = false; });
            while (wait) { yield return null; }
            new LevelManager().LoadLevel(SceneBook.LOADING_NAME);
        }
        else
        {
            //Se deu merda pra criar novo usuario avise o 
            //qual foi o problema
        }

        //Finish load pop, blanck page, painel...
    }

    public void GoBack()
    {
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }
}
