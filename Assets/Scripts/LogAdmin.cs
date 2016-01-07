using UnityEngine;
using System.Collections;

public class LogAdmin : MonoBehaviour {

    public bool test = true;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            AdminUserInput();
    }

    private void AdminUserInput()
    {
        if (test)
        {
            new LevelManager().LoadLevel("MainMenu");
        }
    }
}
