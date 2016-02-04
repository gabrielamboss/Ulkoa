using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MatchHistoryInitializer : MonoBehaviour {

	public GameObject contentParent;
	public GameObject textPrefab;

	// Use this for initialization
	void Start () {
	/*
		MatchDao matchDao = new MatchDao();
		List<Match> matchList = (List<Match>)matchDao.MakeQuerryGetMatchList(Player.getInstance());

		foreach(Match match in matchList){
			Text newMatchID = Instantiate(textPrefab, new Vector3(0,0,0), Quaternion.identity) as Text; //Mudar para deck name
			newMatchID.transform.SetParent(contentParent.transform, false);
			Text newMatchCorrect = Instantiate(textPrefab, new Vector3(0,0,0), Quaternion.identity) as Text;
			newMatchCorrect.transform.SetParent(contentParent.transform, false);
			Text newMatchWrong = Instantiate(textPrefab, new Vector3(0,0,0), Quaternion.identity) as Text;
			newMatchWrong.transform.SetParent(contentParent.transform, false);
			newMatchID.text = match.ObjectId;
			newMatchCorrect.text = match.CorrectPoints.ToString();
			newMatchWrong.text = match.WrongPoints.ToString();
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
