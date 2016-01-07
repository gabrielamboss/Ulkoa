using Parse;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    private bool gogo = false;

    void Awake()
    {
        ParseObject.RegisterSubclass<Match>();
        ParseObject.RegisterSubclass<Card>();
    }

    // Use this for initialization
    void Start () {

        //match.SaveAsync();
        //ParseObject match = ParseObject.Create("Test");
        //match["Test C"] = 10;
        //match["Test W"] = 1;
        //match.SaveAsync();
        //Card card = new Card();
        //string hi = "Hi";
        //card.FrontText = hi;

        Match match = new Match();
        match.CorrectPoints = 10;
        match.WrongPoints = 1;
        Debug.Log(match.CorrectPoints);
        Debug.Log(match.WrongPoints);
        match.SaveAsync().ContinueWith(t=>
        {
            Debug.Log("Save com sucesso");
        });

        //Card card = MockCard("hi1","hi2");


    }

    public void Update()
    {
        if (!gogo)
        {
            
        }
        
    }

    private IEnumerator WaitSubmit()
    {
        while (!gogo)
        {
            yield return null;
        }
    }

}
