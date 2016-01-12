using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Parse;

public class LevelManager : MonoBehaviour {    

	void Start(){
		StartCoroutine(EndSplash());
	}

	IEnumerator EndSplash(){
		if(SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0)){
			yield return new WaitForSeconds(3.0f);
			SceneManager.LoadScene(SceneBook.LOADING_SCREEN_NAME);
		}
	}

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