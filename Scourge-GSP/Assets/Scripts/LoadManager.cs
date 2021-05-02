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
    [SerializeField] Image YouDiedImage;
    [SerializeField] Image VictoryImage;
    Image blackoutSquare;

    // Start is called before the first frame update
    void Start()
    {
        blackoutSquare = GetComponentInChildren<Image>();
        YouDiedImage.enabled = true;
        VictoryImage.enabled = false;
        Color c = YouDiedImage.color;
        c.a = 0f;
        YouDiedImage.color = c;

        //This object needs to persist through loading screens
        DontDestroyOnLoad(this.gameObject);

        //Function is added to list of delegates
        SceneManager.sceneLoaded += OnSceneLoaded;

        //Make sure that Volume Manager attempts to bind to volume slider
        GetComponent<VolumeManager>().BindToVolumeSlider();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        YouDiedImage.enabled = true;
        VictoryImage.enabled = false;
        Color c = YouDiedImage.color;
        c.a = 0f;
        YouDiedImage.color = c;


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
        Color c = YouDiedImage.color;
        c.a = 0f;
        YouDiedImage.color = c;

        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        StartCoroutine(FadeOutBlackoutSquare());

    }

    public void WinToMainMenu()
    {
        YouDiedImage.enabled = false;
        VictoryImage.enabled = true;

        checkPointSceneIndex = -1;
        StartCoroutine(FadeInWinScreen());
    }
    private void LoadMenu()
    {
        YouDiedImage.enabled = false;
        VictoryImage.enabled = false;
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

            if (VictoryImage.color.a <= 1f)
            {
                c = VictoryImage.color;
                c.a += colourAccumulator;
                VictoryImage.color = c;
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

            if (YouDiedImage.color.a <= 1f) 
            {
                c = YouDiedImage.color;
                c.a += colourAccumulator;
                YouDiedImage.color = c;
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
