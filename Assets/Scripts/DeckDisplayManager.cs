using Parse;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckDisplayManager : MonoBehaviour {

	//Public variables
	public GameObject deck;
	public RectTransform contentView;

	//Private variables
	private IEnumerable<Deck> decksCurrUser;

	void Awake () {
		ParseObject.RegisterSubclass<Deck>();
	}

	void Start () {
		ParseUser currUser = ParseUser.CurrentUser;
		var query = new ParseQuery<Deck>().WhereContainedIn("objectId", (currUser.Get<IList<string>>("decksIds") as IList<string>));
		query.FindAsync().ContinueWith(t => {
			if(t.IsCanceled || t.IsFaulted) {
				Debug.Log("Treta na query por Deck. Treta foi: "+t.Exception.ToString());
			} else {
				decksCurrUser = t.Result;
			}
		});
		StartCoroutine(WaitForLoadedDecks());

//		GameObject newItem = Instantiate(deck) as GameObject;
//      newItem.transform.SetParent(contentView.transform, false);
//		Debug.Log(newItem.transform.position.y);
//		newItem.transform.localPosition += new Vector3(0, -90, 0);
//		Debug.Log(newItem.transform.position.y);
//      newItem.SetActive(true);
//
		contentView.sizeDelta = new Vector2(contentView.rect.width, 2000);
	}

	IEnumerator WaitForLoadedDecks() {
		while(decksCurrUser == null) {
			yield return null;
		}
		foreach (ParseObject deck in decksCurrUser) {
			Debug.Log("Olha o ID saído do forno: " + deck.ObjectId);
		}
	}

	void Update () {
	
	}
}
