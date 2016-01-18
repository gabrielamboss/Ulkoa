using Parse;
using UnityEngine.UI;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class RegisterManager : MonoBehaviour {

    public Text username;
    public Text password;
    public Text email;    

    public void Awake()
    {
        ParseObject.RegisterSubclass<Player>();
    }

    public void Save()
    {
        //Save operation
        ParseUser player = new ParseUser();

        player.Username = username.text;
        player.Password = password.text;
        player.Email = email.text;

        player.SignUpAsync().ContinueWith(t=>
        {
            if(t.Exception != null)
            {
                Debug.Log(t.Exception);
            }else if (t.IsCanceled || t.IsFaulted)
            {
                Debug.Log("Problema pra salvar");
            }else
            {
                Debug.Log("Save OK");
            }
        });

        //task.Wait();

        Debug.Log("Fim");

        //Return to LoadingScrean
        new LevelManager().LoadLevel("LoadingScreen");
    }
}
