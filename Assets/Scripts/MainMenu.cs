using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string menuScene;

    public GameObject settingsMenu;

    public void NewGame()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void LoadGame()
    {
        // Todo: Load Game
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
