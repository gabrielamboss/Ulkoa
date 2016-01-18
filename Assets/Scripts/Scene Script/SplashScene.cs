using UnityEngine;
using System.Collections;

public class SplashScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(EndSplashScene());
	}
	
	private IEnumerator EndSplashScene()
    {
        yield return new WaitForSeconds(3.0f);
        new LevelManager().LoadLevel(SceneBook.LOADING_NAME);        
    }
}
