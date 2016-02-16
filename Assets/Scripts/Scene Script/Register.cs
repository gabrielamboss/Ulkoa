using PlayFab;
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

        bool saveSuccessful = false;
        bool wait = true;

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            TitleId = "2071",
            Username = username.text,
            Email = email.text,
            Password = password.text,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(request,
            (result) =>
            {
                saveSuccessful = true;
                wait = false;                
            },
            (error) =>
            {
                saveSuccessful = false;
                wait = false;
                Debug.Log("Erro ao registrar - Ainda nao implementado o tratamento de erro");
                Debug.Log(error.ErrorMessage);
                Debug.Log(error.Error);
                switch (error.Error)
                {
                    case PlayFabErrorCode.InvalidParams:
                        emailError.text = "Error de conexao: Parametros invalidos";
                        break;
                    case PlayFabErrorCode.InvalidTitleId:
                        emailError.text = "Error de conexao: TitleId "+PlayFabSettings.TitleId;
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
            });
        
        while (wait)
        { yield return null; }        

        if (saveSuccessful)
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
                                .addCard("DefPor1", "DefEng1")
                                .addCard("DefPor2", "DefEng2")
                                .addCard("DefPor3", "DefEng3")
                                .addCard("DefPor4", "DefEng4");

        return deckBuilder.getDeck();          
    }

    public void GoBack()
    {
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }
}
