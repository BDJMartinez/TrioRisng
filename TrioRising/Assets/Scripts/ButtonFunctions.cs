using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; // Reference to the Main Menu
    [SerializeField] private GameObject optionsMenu; // Reference to the Options Menu
    [SerializeField] private GameObject creditsMenu; // Reference to the Credits Menu

    // Start is called before the first frame update
    public void resume()
    {
        gamemanager.instance.stateUnpause();
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
        gamemanager.instance.stateUnpause();
    }

    public void quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else   
        Application.Quit();
    #endif
    }

    public void respawn()
    {
        gamemanager.instance.playerScript.spawnPlayer();
        gamemanager.instance.stateUnpause();
    }

    public void openOptions()
    {
        mainMenu.SetActive(false); // Hide the main menu
        optionsMenu.SetActive(true); // Show the options menu
    }

    public void backToMainMenu()
    {
        optionsMenu.SetActive(false); // Hide the options menu
        mainMenu.SetActive(true); // Show the main menu
    }

    public void openCredits()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void backToOptions()
    {
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

}
