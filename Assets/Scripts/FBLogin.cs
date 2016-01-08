using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System.Threading.Tasks;
using Parse;
using UnityEngine.SceneManagement;

public class FBLogin : MonoBehaviour {

	
	
	void Awake(){
		if(!FB.IsInitialized){
			FB.Init(SetInit, OnHideUnity);
		}
		else{
			//FB.ActivateApp();
		}

	}

	private void SetInit(){
		Debug.Log("FB Init called");
		if(FB.IsInitialized){
			Debug.Log("Facebook SDK Initialized");
		}
		else{
			Debug.LogWarning("Failed to Initialize Facebook SDK");
		}

	}


	public void InitLogin(){
		if(FB.IsLoggedIn){
			Debug.Log("Init called with login already done");
		}
		else{
			var permissions = new List<string>(){"public_profile", "email"};
			FB.LogInWithReadPermissions(permissions, AuthCallback);
		}
	}



	private void AuthCallback(ILoginResult result){
		if(FB.IsLoggedIn){
			var accessToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			GlobalVariables.facebookLogin = true;

			Debug.Log("Facebook Acess token User ID: "+ accessToken.UserId);
			Debug.Log("Permissions:");
			foreach(string perm in accessToken.Permissions){
				Debug.Log(perm);
			}

			Task<ParseUser> logInTask = ParseFacebookUtils.LogInAsync(accessToken.UserId, accessToken.TokenString, accessToken.ExpirationTime);

			SceneManager.LoadScene(SceneBook.MAIN_MENU_NAME);
		}
		else{
			Debug.Log("Login cancelled.");
		}
	}

	private void OnHideUnity(bool isGameShown){
		if(!isGameShown)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
}
