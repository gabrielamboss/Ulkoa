using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loading : MonoBehaviour {

    public Text loadLabel;
    private bool runLabel;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadingLabel());
        StartCoroutine(InitializeGameAndContinueToMainMenu());
    }


    private IEnumerator InitializeGameAndContinueToMainMenu()
    {

        yield return UlkoaInitializer.InitializeGame();

        if (UlkoaInitializer.isSuccessfull())
            new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);

        else
        {
            runLabel = false;
            yield return new WaitForSeconds(0.4f);
            loadLabel.text = "Error de conexão";
            yield return new WaitForSeconds(3.0f);
            new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
        }
    }

    private IEnumerator WaitAndContinueToLoginScene()
    {
        yield return new WaitForSeconds(3.0f);
        new LevelManager().LoadLevel(SceneBook.LOGIN_NAME);
    }

    private IEnumerator LoadingLabel()
    {
        runLabel = true;
        loadLabel.text = "";
        while (runLabel)
        {
            yield return new WaitForSeconds(0.3f);
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        string text = "Carregando...";
        int count = loadLabel.text.Length;
        if (count == text.Length)
        {
            count = 0;
            loadLabel.text = "";
        }
        Debug.Log("Text.Length " + text.Length);
        Debug.Log("Count "+count);
        loadLabel.text += text[count];
    }
}
