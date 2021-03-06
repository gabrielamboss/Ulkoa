﻿using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour {

    public GameObject painel;

    public InputField username = null;
    public InputField password = null;
    public InputField email = null;

    public Text userError;
    public Text passwordError;
    public Text emailError;    

    private bool succesfull;

    void Start()
    {
        username.Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Save();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.isFocused)
                password.Select();
            else if (password.isFocused)
                email.Select();
            else if (email.isFocused)
                username.Select();
        }
    }

    public void Save()
    {
        Debug.Log("Init save");

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

        if (email.text.Equals(""))
        {
            emailError.text = "Campo email em branco";
            inputIsCorrect = false;
        }

        Debug.Log("Init save2");
        if (inputIsCorrect)
            StartCoroutine(SaveLogic());
    }

    private IEnumerator SaveLogic()
    {        
        painel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();        
                
        Authenticator authenticator = new Authenticator()
                                            .setUserName(username.text)
                                            .setPassword(password.text)
                                            .setEmail(email.text)
                                            .setRegisterSuccesfullCallback(registerSuccesfull)
                                            .setRegisterFailCallback(registerFail);

        yield return authenticator.makeRegister();

        if (succesfull)
        {            
            yield return savePlayerData();
            new LevelManager().LoadLevel(SceneBook.LOADING_NAME);            
        }
                
        painel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

    private IEnumerator savePlayerData()
    {
        Player player = new Player();
        player.Username = username.text;

        Deck defaultDeck = getDefaultDeck();
        player.addDeck(defaultDeck);        

        PlayerDao playerDao = new PlayerDao();        
        yield return playerDao.savePlayer(player);                
    }

    private Deck getDefaultDeck()
    {

        DeckBuilder deckBuilder = new DeckBuilder()
                                .setDeckName("Default")
                                .setDeckEditable(false)
                                .addCard("DefPor1", "DefEng1")
                                .addCard("DefPor2", "DefEng2")
                                .addCard("DefPor3", "DefEng3")
                                .addCard("DefPor4", "DefEng4");

        return deckBuilder.getDeck();          
    }

    //Infelizmente eu nao posso chamar savePlayerData daqui de dentro
    //por isso tive que fazer essa gambiarra
    private void registerSuccesfull(RegisterPlayFabUserResult result)
    {
        succesfull = true;
    }

    private void registerFail(PlayFabError error)
    {
        succesfull = false;

        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidParams:
                emailError.text = "Parametros invalidos, porfavor verifique seu email";
                break;
            case PlayFabErrorCode.InvalidTitleId:
                emailError.text = "Error de conexao: TitleId " + PlayFabSettings.TitleId;
                break;
            case PlayFabErrorCode.EmailAddressNotAvailable:
                emailError.text = "Email nao disponivel";
                break;
            case PlayFabErrorCode.InvalidEmailAddress:
                emailError.text = "Email invalido";
                break;
            case PlayFabErrorCode.UsernameNotAvailable:
                userError.text = "Usuario ja existente";
                break;
            case PlayFabErrorCode.InvalidUsername:
                userError.text = "Usuario ja existente";
                break;
            case PlayFabErrorCode.InvalidPassword:
                passwordError.text = "Senha invalida";
                break;
            default:
                break;
        }

    }

    public void GoBack()
    {
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }
}
