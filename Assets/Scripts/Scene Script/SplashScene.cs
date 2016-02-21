using PlayFab;
using UnityEngine;
using System.Collections;

public class SplashScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayFabSettings.TitleId = "2071";        
        StartCoroutine(EndSplashScene());
	}
	
	private IEnumerator EndSplashScene()
    {
        yield return new WaitForSeconds(3.5f);
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);        
    }
}
