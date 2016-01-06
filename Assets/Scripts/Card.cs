using Parse;
using UnityEngine;
using System.Collections;

[ParseClassName("Card")]
public class Card : ParseObject {
	
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	[ParseFieldName("FrontText")]
  	public string FrontText {
		get { return GetProperty<string>("FrontText"); }
   		set { SetProperty<string>(value, "FrontText"); }
	}

	[ParseFieldName("BackText")]
	public string BackText {
		get { return GetProperty<string>("BackText"); }
   		set { SetProperty<string>(value, "BackText"); }
	}

	[ParseFieldName("FrontUrl")]
	public string FrontUrl {
		get { return GetProperty<string>("FrontUrl"); }
   		set { SetProperty<string>(value, "FrontUrl"); }
	}

	[ParseFieldName("BackUrl")]
	public string BackUrl {
		get { return GetProperty<string>("BackUrl"); }
   		set { SetProperty<string>(value, "BackUrl"); }
	}

	[ParseFieldName("Font")]
	public string Font {
		get { return GetProperty<string>("Font"); }
   		set { SetProperty<string>(value, "Font"); }
	}

	[ParseFieldName("Title")]
	public string Title {
		get { return GetProperty<string>("Title"); }
   		set { SetProperty<string>(value, "Title"); }
	}

	[ParseFieldName("Description")]
	public string Description {
		get { return GetProperty<string>("Description"); }
   		set { SetProperty<string>(value, "Description"); }
	}
}