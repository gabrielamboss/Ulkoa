using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {    

	public void LoadLevel(string name) {
		Debug.Log("New Level load: " + name);
        //float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest() {
		Debug.Log("Quit requested");
		Application.Quit ();
	}
}