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
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnClick(Button btn)
    {
        switch (btn.tag)
        {
            case "Resume":
                isOpen = false;
                break;
            case "MainMenu":
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
                break;
            case "Quit":
                Application.Quit();
                break;
        }
    }

}
