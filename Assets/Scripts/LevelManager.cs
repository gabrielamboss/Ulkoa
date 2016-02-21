using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Parse;

public class LevelManager : MonoBehaviour {
    public GameObject panel;    	

	public void LoadLevel(string name) {
		Debug.Log("New Level load: " + name);
		SceneManager.LoadScene(name);
	}

    public void LeaveEndGame(string name)
    {
        GlobalVariables.PremiumWasDisplayed = false;
        StartCoroutine(new PlayerDao().savePlayer(Player.getInstance()));
        Debug.Log("New Level load: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest() {
		Debug.Log("Quit requested");
		Application.Quit ();
	}
    
}