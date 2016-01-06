using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private string username;
	private string email;
	private string password;
	private string id;
	private int timeSpentInGame;
	private bool isFacebook;
	private IList<string> loginDates;
	private IList<string> acquisitions;
	private IList<string> achievements;
	private IList<string> friends;
	private IDictionary<string, string> specialItems;
	private IList<Deck> decks;
	private string[] trainingDevelopment;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string Username {
		get {
			return this.username;
		}
		set {
			username = value;
		}
	}

	public string Email {
		get {
			return this.email;
		}
		set {
			email = value;
		}
	}

	public string Password {
		get {
			return this.password;
		}
		set {
			password = value;
		}
	}

	public string Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public int TimeSpentInGame {
		get {
			return this.timeSpentInGame;
		}
		set {
			timeSpentInGame = value;
		}
	}

	public bool IsFacebook {
		get {
			return this.isFacebook;
		}
		set {
			isFacebook = value;
		}
	}

	public IList<string> LoginDates {
		get {
			return this.loginDates;
		}
		set {
			loginDates = value;
		}
	}

	public IList<string> Acquisitions {
		get {
			return this.acquisitions;
		}
		set {
			acquisitions = value;
		}
	}

	public IList<string> Achievements {
		get {
			return this.achievements;
		}
		set {
			achievements = value;
		}
	}

	public IList<string> Friends {
		get {
			return this.friends;
		}
		set {
			friends = value;
		}
	}

	public IDictionary<string, string> SpecialItems {
		get {
			return this.specialItems;
		}
		set {
			specialItems = value;
		}
	}

	public IList<Deck> Decks {
		get {
			return this.decks;
		}
		set {
			decks = value;
		}
	}

	public string[] TrainingDevelopment {
		get {
			return this.trainingDevelopment;
		}
		set {
			trainingDevelopment = value;
		}
	}
}
