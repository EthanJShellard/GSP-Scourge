using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private MonoBehaviour[] playerBehaviours = new MonoBehaviour[3];

    bool isOpen = false;

    void Start()
    {
        pauseMenu.SetActive(false);

        foreach (Button b in GetComponentsInChildren<Button>()) 
        {
            b.image.alphaHitTestMinimumThreshold = 0.5f;
        }

        Player player = FindObjectOfType<Player>();
        playerBehaviours[0] = player.GetComponent<PlayerController>();
        playerBehaviours[1] = player.GetComponent<PlayerAttackStick>();
        playerBehaviours[2] = player.GetComponent<PlayerShoot>();
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

        playerBehaviours[0].enabled = false;
        playerBehaviours[1].enabled = false;
        playerBehaviours[2].enabled = false;
    }

    public void ClosePauseMenu() 
    {
        isOpen = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        playerBehaviours[0].enabled = true;
        playerBehaviours[1].enabled = true;
        playerBehaviours[2].enabled = true;
    }

    public void Quit() 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }

}
