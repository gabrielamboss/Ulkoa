using Parse;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class DisplayUser : MonoBehaviour
{
    public Text userLabel;

    // Use this for initialization
    void Start()
    {
        userLabel.text = "Logged in as " + ParseUser.CurrentUser.Username;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
