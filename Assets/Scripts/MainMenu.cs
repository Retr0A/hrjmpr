using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string mainScene;

    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameManager gameManager;

    public void NewGame()
    {
        SceneManager.LoadScene(mainScene);
    }

    public void LoadGame()
    {
        // Todo: Load Game
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ToggleOptionsMenu()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void ToggleShouldSHowFPS()
    {
        gameManager.settingsObject.shouldShowFPS = !gameManager.settingsObject.shouldShowFPS;
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        gameManager.settingsObject.shouldUseFullscreen = fullscreen;
    }
}
