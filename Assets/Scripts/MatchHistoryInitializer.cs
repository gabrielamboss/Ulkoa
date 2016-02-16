using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MatchHistoryInitializer : MonoBehaviour {

	private static bool hasInitialized = false;
	private Deck currDeck;

	public GameObject contentParent;
	public GameObject textPrefab;

	public IEnumerator Initialize(){
        
		MatchDao matchDao = new MatchDao();        
		List<Match> matchList = matchDao.getMatchsByDeck(currDeck);
        

		foreach(Match match in matchList){
			Text newDeckID = Instantiate(textPrefab, new Vector3(0,0,0), Quaternion.identity) as Text; 
			newDeckID.transform.SetParent(contentParent.transform, false);
			Text newMatchCorrect = Instantiate(textPrefab, new Vector3(0,0,0), Quaternion.identity) as Text;
			newMatchCorrect.transform.SetParent(contentParent.transform, false);
			Text newMatchWrong = Instantiate(textPrefab, new Vector3(0,0,0), Quaternion.identity) as Text;
			newMatchWrong.transform.SetParent(contentParent.transform, false);
			//newDeckID.text = currDeck.ObjectId; Deck nao possui mais campo Id
			newMatchCorrect.text = match.CorrectPoints.ToString();
			newMatchWrong.text = match.WrongPoints.ToString();
		}

		hasInitialized = true;

        return null;
	}

	public static bool HasInitialized(){
		return hasInitialized;
	}

	private IEnumerator InitializeScene()
    {
        yield return Initialize();

        while (!HasInitialized())
        { yield return null;}

    }

	// Use this for initialization
	void Start () {
		InitializeScene();
	}
	
}
