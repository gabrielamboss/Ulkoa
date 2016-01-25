using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Parse;

public class Loading : MonoBehaviour {

    private Text load;
    private bool labelOn;    

	// Use this for initialization
	void Start () {        
        GameObject aux = GameObject.Find("Loading Label");
        load = aux.GetComponent<Text>();
        
        StartCoroutine(LoadLabel());
                
        if (ParseUser.CurrentUser != null)
        {            
            StartCoroutine(UserIsLogged());
        }
        else
        {            
            StartCoroutine(UserIsNotLogged());
        }
        
	}
	
    
	private IEnumerator UserIsLogged()
    {
        yield return UlkoaInitializer.InitializeGame();

        while (!UlkoaInitializer.hasInitialied())
        { yield return null;}

        labelOn = false;
        new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);        
    }

    private IEnumerator UserIsNotLogged()
    {
        yield return new WaitForSeconds(3.0f);
        labelOn = false;
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }

    private IEnumerator LoadLabel()
    {
        labelOn = true;
        load.text = "";
                
        while (labelOn)
        {
            yield return new WaitForSeconds(0.3f);            
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        string text = "Carregando...";
        int count = load.text.Length;
        if (count == text.Length)
        {
            count = 0;
            load.text = "";
        }
        load.text += text[count];
        count++;
    }
}
