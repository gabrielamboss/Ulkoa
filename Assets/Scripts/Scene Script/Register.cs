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
        Debug.Log("Init save logic");
        painel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();
        Debug.Log("Painel");

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
                Debug.Log("Username = "+result.Username);                
            },
            (error) =>
            {
                saveSuccessful = false;
                wait = false;
                Debug.Log("Erro ao registrar - Ainda nao implementado o tratamento de erro");
                Debug.Log(error.ErrorMessage);
                Debug.Log(error.Error);
            });

        Debug.Log("Wait");
        while (wait)
        { yield return null; }
        Debug.Log("Wait finish");

        if (saveSuccessful)
        {
            //Se conseguimos criar um novo usuario crie para ele um player
            //com as informacoes necessarias do jogador
            Player player = new Player();
            player.Username = username.text;

            Deck defaultDeck = getDefaultDeck();            
            player.addDeck(defaultDeck);
            player.addToStoreDeckNameList(defaultDeck.DeckName);

            PlayerDao playerDao = new PlayerDao();
            Debug.Log("Trying to save player");
            yield return playerDao.savePlayer(player);
            Debug.Log("Finish save player");

            Debug.Log("Finish");
            new LevelManager().LoadLevel(SceneBook.LOADING_NAME);
            /*
            Player player = Player.createNewPlayer(user);
            PlayerDao playerDao = new PlayerDao();
            yield return playerDao.savePlayer(player);
            Player.setInstance(player);

            //Crie tambem um default deck
            Deck defaultDeck = getDefaultDeck();
            DeckDao deckDao = new DeckDao();
            yield return deckDao.saveDeck(defaultDeck);
            
            new LevelManager().LoadLevel(SceneBook.LOADING_NAME);
            */
        }
        else
        {            
            //Se deu merda pra criar novo usuario avise o 
            //qual foi o problema
            /*
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
            */
        }
        
        painel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
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
