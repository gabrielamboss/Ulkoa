using Parse;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ParseClassName("CardCollection")]
public class CardCollection : ParseObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[ParseFieldName("CardIds")]
	public IList<string> CardIds {
		get { return GetProperty<IList<string>>("CardIds"); }
   		set { SetProperty<IList<string>>(value, "CardIds"); }
  	}

}