using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectionMenu : MonoBehaviour {

    public bool test = false;
    
    // Use this for initialization
    void Start () {
        if (test)
        {            
           Player2.Init();           
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
