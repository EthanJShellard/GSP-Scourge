using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class to track player progress and return to desired states
/// </summary>
public class LoadManager : MonoBehaviour
{
    Vector3 respawnPosition;
    //Used to keep track of which scene our respawn position is saved in
    int checkPointSceneIndex;

    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;
    Image blackoutSquare;
    TextMeshProUGUI deathText;

    // Start is called before the first frame update
    void Start()
    {
        blackoutSquare = GetComponentInChildren<Image>();
        deathText = GetComponentInChildren<TextMeshProUGUI>();
        deathText.text = "You Died";

        //This object needs to persist through loading screens
        DontDestroyOnLoad(this.gameObject);

        //Function is added to list of delegates
        SceneManager.sceneLoaded += OnSceneLoaded;

        //Make sure that Volume Manager attempts to bind to volume slider
        GetComponent<VolumeManager>().BindToVolumeSlider();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        LoadManager[] loadManagers = FindObjectsOfType<LoadManager>();
        if (loadManagers.Length > 1) 
        {
            for (int i = 1; i < loadManagers.Length; i++) 
            {
                DestroyImmediate(loadManagers[i].gameObject);
            }
        }

        //If we have a checkpoint in this scene
        if (scene.buildIndex == checkPointSceneIndex) 
        {
            Player p = FindObjectOfType<Player>();
            if(p) p.transform.position = respawnPosition;
        }

        //Make sure that Volume Manager attempts to bind to volume slider
        GetComponent<VolumeManager>().BindToVolumeSlider();
    }

    public void SetRespawnPosition(Vector3 pos) 
    {
        respawnPosition = pos;
        checkPointSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void ReloadToCheckpoint() 
    {
        StartCoroutine(FadeInDeathScreen());
        Time.timeScale = 1;
    }

    private void ReloadScene() 
    {
        Color c = deathText.color;
        c.a = 0f;
        deathText.color = c;

        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        StartCoroutine(FadeOutBlackoutSquare());

    }

    public void WinToMainMenu()
    {
        deathText.text = "Level Complete!";
        StartCoroutine(FadeInWinScreen());
    }
    private void LoadMenu()
    {
        Color c = deathText.color;
        c.a = 0f;
        deathText.color = c;
        SceneManager.LoadScene(0);
        StartCoroutine(FadeOutBlackoutSquare());
    }

    IEnumerator FadeInWinScreen()
    {
        float timer = 0f;
        float timeAccumulator = 0.05f / fadeInTime;
        float colourAccumulator = 0.01f;
        Color c;

        while (timer < fadeInTime)
        {
            if (blackoutSquare.color.a <= 1f)
            {
                c = blackoutSquare.color;
                c.a += colourAccumulator;
                blackoutSquare.color = c;
            }

            if (deathText.color.a <= 1f)
            {
                c = deathText.color;
                c.a += colourAccumulator;
                deathText.color = c;
            }


            timer += timeAccumulator;
            yield return new WaitForSecondsRealtime(timeAccumulator);
        }

        LoadMenu();
    }

    IEnumerator FadeInDeathScreen() 
    {
        float timer = 0f;
        float timeAccumulator = 0.05f / fadeInTime;
        float colourAccumulator = 0.01f;
        Color c;

        while(timer < fadeInTime)
        {
            if (blackoutSquare.color.a <= 1f) 
            {
                c = blackoutSquare.color;
                c.a += colourAccumulator;
                blackoutSquare.color = c;
            }

            if (deathText.color.a <= 1f) 
            {
                c = deathText.color;
                c.a += colourAccumulator;
                deathText.color = c;
            }
            

            timer += timeAccumulator;
            yield return new WaitForSecondsRealtime(timeAccumulator);
        }

        ReloadScene();
    }

    IEnumerator FadeOutBlackoutSquare() 
    {
        float timer = 0f;
        float accumulator = fadeOutTime / 255;
        Color c;
        
        while (timer < fadeOutTime) 
        {
            c = blackoutSquare.color;
            c.a -= accumulator;
            blackoutSquare.color = c;

            timer += accumulator;
            yield return new WaitForSecondsRealtime(accumulator);
        }
        blackoutSquare.color = Color.clear;
    }
}
