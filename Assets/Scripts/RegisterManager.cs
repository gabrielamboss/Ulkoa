using UnityEngine;
using System.Collections;

public class RegisterManager : MonoBehaviour {

	public void Save()
    {
        //Save operation

        //Return to LoadingScrean
        new LevelManager().LoadLevel("LoadingScreen");
    }
}
