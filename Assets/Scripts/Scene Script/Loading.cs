using Parse;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loading : MonoBehaviour {

    public Text loadLabel;     

	// Use this for initialization
	void Start () {                        
        StartCoroutine(LoadingLabel());
                
        if (ParseUser.CurrentUser != null)        
            StartCoroutine(InitializeGameAndContinueToMainMenu());
        
        else                    
            StartCoroutine(WaitAndContinueToLoginScene());
                
	}
	
    
	private IEnumerator InitializeGameAndContinueToMainMenu()
    {
        yield return UlkoaInitializer.InitializeGame();

        while (!UlkoaInitializer.HasInitialized())
        { yield return null;}
        
        new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);        
    }

    private IEnumerator WaitAndContinueToLoginScene()
    {
        yield return new WaitForSeconds(3.0f);        
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }

    private IEnumerator LoadingLabel()
    {

        loadLabel.text = "";                
        while (true)
        {
            yield return new WaitForSeconds(0.3f);            
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        string text = "Carregando...";
        int count = loadLabel.text.Length;
        if (count == text.Length)
        {
            count = 0;
            loadLabel.text = "";
        }
        loadLabel.text += text[count];        
    }
}
