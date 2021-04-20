using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    bool isOpen = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen) ClosePauseMenu();
            else OpenPauseMenu();
        }
    }

    public void GoToMainMenu() 
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OpenPauseMenu() 
    {
        isOpen = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu() 
    {
        isOpen = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit() 
    {
        Application.Quit();
    }

}
