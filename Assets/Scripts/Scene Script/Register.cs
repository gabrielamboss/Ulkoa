using Parse;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

public class Register : MonoBehaviour {

    public GameObject painel;
    private InputField username = null;
    private InputField password = null;
    private InputField email = null;

    public Text userError;
    public Text passwordError;
    public Text emailError;

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

        if (inputIsCorrect)
            StartCoroutine(SaveLogic());
    }

    private IEnumerator SaveLogic()
    {        
        painel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();

        bool saveSuccessful = false;
        bool wait = false;
        AggregateException erros = null;

        //Tentar criar novo usuario
        ParseUser user = new ParseUser();

        user.Username = username.text;
        user.Password = password.text;
        user.Email = email.text;

        wait = true;
        user.SignUpAsync().ContinueWith(t =>
        {
            saveSuccessful = !(t.IsCanceled || t.IsFaulted);
            erros = t.Exception;
            wait = false;
        });
        while (wait) { yield return null; }

        if (saveSuccessful)
        {
            //Se conseguimos criar um novo usuario crie para ele um player
            //com as informacoes necessarias do jogador
            yield return Player.createNewPlayer(ParseUser.CurrentUser);
            yield return createDefaultDeck();
            new LevelManager().LoadLevel(SceneBook.LOADING_NAME);
        }
        else
        {            
            //Se deu merda pra criar novo usuario avise o 
            //qual foi o problema
            foreach (Exception e in erros.InnerExceptions)
            {
                if (e is ParseException)
                    switch ((e as ParseException).Code)
                    {                        
                        case ParseException.ErrorCode.UsernameTaken:
                            userError.text = "Usuario ja existente";
                            break;                        
                        case ParseException.ErrorCode.EmailTaken:
                            emailError.text = "Email ja existente";
                            break;                        
                        case ParseException.ErrorCode.InvalidEmailAddress:
                            emailError.text = "Endereço de email invalido";
                            break;
                        default:
                            emailError.text = "Ocorreu um erro durante o registro, por favor tente de novo";
                            break;
                    }
                else emailError.text = "Ocorreu um erro durante o registro, por favor tente de novo";
            }
        }
        
        painel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

    private IEnumerator createDefaultDeck()
    {

        DeckBuilder deckBuilder = new DeckBuilder()
                                .setDeckName("Default")
                                .addCard("DefPor1", "DefEng1")
                                .addCard("DefPor2", "DefEng2")
                                .addCard("DefPor3", "DefEng3")
                                .addCard("DefPor4", "DefEng4");

        DeckDao deckDao = new DeckDao();
        yield return deckDao.saveDeck(deckBuilder.getDeck());           

    }

    public void GoBack()
    {
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }
}
