using Parse;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ParseClassName("Player")]
public class Player : ParseUser {

	//private string username;
	//private string email;
	//private string password;
	//private string id;
	//private int timeSpentInGame;
	//private bool isFacebook;
	//private IList<string> loginDates;
	//private IList<string> acquisitions;
	//private IList<string> achievements;
	//private IList<string> friends;
	private IDictionary<string, string> specialItems;
	private IList<Deck> decks;
	private string[] trainingDevelopment;

    [ParseFieldName("TimeSpentInGame")]
    public int TimeSpentInGame {
        get { return GetProperty<int>("TimeSpentInGame"); }
        set { SetProperty<int>(value, "TimeSpentInGame"); }
    }

    [ParseFieldName("IsFacebook")]
    public bool IsFacebook {
        get { return GetProperty<bool>("IsFacebook"); }
        set { SetProperty<bool>(value, "IsFacebook"); }
    }

    [ParseFieldName("LoginDates")]
    public IList<string> LoginDates {
        get { return GetProperty<IList<string>>("LoginDates"); }
        set { SetProperty<IList<string>>(value, "LoginDates"); }
    }

    [ParseFieldName("Acquisitions")]
    public IList<string> Acquisitions {
        get { return GetProperty<IList<string>>("Acquisitions"); }
        set { SetProperty<IList<string>>(value, "Acquisitions"); }
    }

    [ParseFieldName("Achievements")]
    public IList<string> Achievements {
        get { return GetProperty<IList<string>>("Achievements"); }
        set { SetProperty<IList<string>>(value, "Achievements"); }
    }

    [ParseFieldName("Friends")]
    public IList<string> Friends {
        get { return GetProperty<IList<string>>("Friends"); }
        set { SetProperty<IList<string>>(value, "Friends"); }
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
