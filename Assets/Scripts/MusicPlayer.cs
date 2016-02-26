using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip mainTheme, gameMusic, deckCreatorMusic, splashSFX;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource>();
		audioSource.loop = false;
		audioSource.volume = 0.4f;
		audioSource.clip = splashSFX;
		audioSource.Play();
		DontDestroyOnLoad(transform.gameObject);
	}

	void OnLevelWasLoaded(int scene){
		audioSource.loop = true;
		AudioClip prevClip = audioSource.clip;
		if(scene == SceneBook.GAME_INDEX)
			audioSource.clip = gameMusic;
		else if(scene == SceneBook.MAIN_MENU_INDEX)
			audioSource.clip = mainTheme;
		else if(scene == SceneBook.DECK_CREATOR_INDEX)
			audioSource.clip = deckCreatorMusic;
		else
			audioSource.clip = mainTheme;

		if(prevClip != audioSource.clip)
			audioSource.Play();
	}

}
