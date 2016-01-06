using UnityEngine;
using Parse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ParseInitializer : MonoBehaviour {

	void Awake () {
		ParseObject.RegisterSubclass<Card>();
		ParseObject.RegisterSubclass<Deck>();
		ParseObject.RegisterSubclass<CardCollection>();
	}
	
	void Start () {
		//Places 'Loading' above everything while not logged in
		//TODO: Create Loading		

		//Start a User in Parse
		//ParseBasics();
		StartUser();
	}
	
	void StartUser() {
		//LogOut a user
		//ParseUser.LogOut();

        
		//Already Logged in:
		if (ParseUser.CurrentUser != null) {

			//Does Logged in Stuff
			var currUser = ParseUser.CurrentUser;
			Debug.Log("Logged in as: "+currUser.Username);
			currUser.SaveAsync();

		//Not Logged in		
		} else {
			Debug.Log("Not Logged in. :(");
			
			ParseUser.LogInAsync("ulkoa_test07", "regassa").ContinueWith(t => {
				if (t.IsFaulted || t.IsCanceled) {
					//The login failed. Check the error to see why.
					if(t.IsFaulted) {
						Debug.Log("Login failed because Faulted. Details: "+t.Exception.ToString());
						if(t.Exception.ToString().Contains("invalid login parameters")) {
							//Ask user to create account
						}
					} else if(t.IsCanceled) {
						Debug.Log("Login failed because Canceled. Details: "+t.Exception.Message);
					}
				} else {
					//Login was successful.
					var currUser = ParseUser.CurrentUser;
					Debug.Log("Successful login as: "+currUser.Username);
				}
			});
		}
        
    }

    void Update () {
	
	}

	void ParseBasics() {
		//Creates an Object
		//ParseObject testObject = new ParseObject("TestObject");
		//testObject["fo"] = "deu";
		//testObject.SaveAsync();

		//Creates an User
		//var user = new ParseUser() {
		//	//Username is treated like ID, beware!
		//	Username = "ulkoa_test07",
		//	//Email is treated like ID, beware!
		//	Email = "email07@test.com",
		//	//MonoDevelop bitches about Password, 
		//	//but have faith and just compile anyway!
		//	Password = "regassa",
		//};
		//var cardCollection = new CardCollection();
		//cardCollection.CardIds = new List<string>();
		//cardCollection.SaveAsync().ContinueWith(t => {
		//	user["collectionId"] = cardCollection.ObjectId;
		//	user.SignUpAsync().ContinueWith(t2 => {
		//		if(t2.IsCompleted) {
		//			Debug.Log("Sign up successfull!");
		//		}
		//	});
		//});

		//Simple Query
		//Debug.Log("before query");
		//ParseUser.Query.WhereEqualTo("username", "our name")
		//.FindAsync().ContinueWith(t => {
		//	Debug.Log("query completed");
		//	IEnumerable<ParseUser> players = t.Result;
		//	foreach(ParseUser player in players) {
		//		Debug.Log(player.Email);
		//	}
		//});		
	}
}
