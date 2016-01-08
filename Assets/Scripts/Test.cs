using Parse;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    private bool gogo = false;

    void Awake()
    {        
        ParseObject.RegisterSubclass<Player>();
    }

    // Use this for initialization
    void Start () {

        if (ParseUser.CurrentUser != null)
        {
            Debug.Log("Estou logado");
            Player player = ParseUser.CurrentUser as Player;
            if(player != null)
            {
                Debug.Log("Deu certo");
                Debug.Log(player.TimeSpentInGame);
            }
            else
            {
                Debug.Log("FAIL");
            }
            
        }          

    }        

}
