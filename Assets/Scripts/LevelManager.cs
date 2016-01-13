using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Parse;

public class LevelManager : MonoBehaviour {    	

	public void LoadLevel(string name) {
		Debug.Log("New Level load: " + name);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest() {
		Debug.Log("Quit requested");
		Application.Quit ();
	}

    // Load the next level from the Loading Scene
    public void LoadingSceneLoadNextLevel()
    {
        LoginManager logMng = new LoginManager();

        if (logMng.IsPlayerLoggedIn())
        {
            LoadLevel(SceneBook.MAIN_MENU_NAME);
        }
        else
        {
            LoadLevel(SceneBook.LOGIN_NAME);
        }
    }
}