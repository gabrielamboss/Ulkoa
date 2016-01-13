using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Parse;

public class Loading : MonoBehaviour {

    private Text load;
    private bool labelOn;
    private int count = 0;

	// Use this for initialization
	void Start () {
        GameObject aux = GameObject.Find("Loading Label");
        load = aux.GetComponent<Text>();
        
        StartCoroutine(LoadLabel());

        if (ParseUser.CurrentUser != null)
        {

        }
	}
	
	private IEnumerator UserIsLogged()
    {
        return null;
    }

    private IEnumerator LoadLabel()
    {
        labelOn = true;
        load.text = "";
        while (labelOn)
        {
            yield return new WaitForSeconds(0.3f);
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        if (count == 12)
        {
            count = 0;
            load.text = "";
        }
        load.text += "Carregando..."[count];
        count++;
    }
}
