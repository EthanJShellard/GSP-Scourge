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

        //This object needs to persist through loading screens
        DontDestroyOnLoad(this);

        //Function is added to list of delegates
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        //If we have a checkpoint in this scene
        if (scene.buildIndex == checkPointSceneIndex) 
        {
            Player p = FindObjectOfType<Player>();
            p.transform.position = respawnPosition;
            p.HealFull();
        }
    }

    public void SetRespawnPosition(Vector3 pos) 
    {
        respawnPosition = pos;
        checkPointSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void ReloadToCheckpoint() 
    {
        StartCoroutine(FadeInDeathScreen());
        Time.timeScale = 0;
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

    IEnumerator FadeInDeathScreen() 
    {
        float timer = 0f;
        float accumulator = fadeInTime / 255;
        Color c;

        while(timer < fadeInTime)
        {
            c = blackoutSquare.color;
            c.a += accumulator;
            blackoutSquare.color = c;

            c = deathText.color;
            c.a += accumulator;
            deathText.color = c;

            timer += accumulator;
            yield return new WaitForSecondsRealtime(accumulator);
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
        Debug.Log("Done");
    }
}
