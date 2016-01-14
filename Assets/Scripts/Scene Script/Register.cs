using Parse;
using UnityEngine.UI;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class Register : MonoBehaviour {

    private InputField username = null;
    private InputField password = null;
    private InputField email = null;

    private bool wait;
    private bool saveSuccessful;

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
        saveSuccessful = false;
        wait = false;

        Debug.Log("Start save operation");
        //Save operation
        ParseUser player = new ParseUser();

        player.Username = username.text;
        player.Password = password.text;
        player.Email = email.text;

        wait = true;
        player.SignUpAsync().ContinueWith(t=>
        {
            if (t.IsCanceled || t.IsFaulted)
            {
                Debug.Log("Problema pra salvar");                
            }
            else
            {
                saveSuccessful = true;
            }
            wait = false;
        });
        StartCoroutine(WaitSave());  
    }

    private IEnumerator WaitSave()
    {
        while (wait)
        {
            yield return null;            
        }
        if (saveSuccessful)
        {
            new LevelManager().LoadLevel(SceneBook.LOADING_SCREEN_NAME);
        }
        else
        {
            //Do some thing
        }
    }

    public void GoBack()
    {
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }
}
