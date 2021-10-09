using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public Button continueButton;
    public Button newGameButton;
    public Button settingsButton;
    public Button exitButton;

    public GameObject settingsScreen;

    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(delegate { ContinueGame(); });
        newGameButton.onClick.AddListener(delegate { StartNewGame(); });
        settingsButton.onClick.AddListener(delegate { OpenSettings(); });
        exitButton.onClick.AddListener(delegate { ExitGame(); });
    }

    private void OnEnable() {
        CheckForSavedGame();
    }

    private void CheckForSavedGame() {
        //example
        continueButton.gameObject.SetActive(System.IO.File.Exists(Application.persistentDataPath + "/ExampleFileSaveName.save"));

    }

    private void ContinueGame() {
        //load save and for example load scene
    }

    private void StartNewGame() {
        //load your scene
        SceneManager.LoadScene("SampleScene");
    }


    private void OpenSettings() {
        gameObject.SetActive(false);
        settingsScreen.SetActive(true);
    }


    private void ExitGame() {
        Application.Quit();
    }
}
