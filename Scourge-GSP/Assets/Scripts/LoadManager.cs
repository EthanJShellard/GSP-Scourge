using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to track player progress and return to desired states
/// </summary>
public class LoadManager : MonoBehaviour
{
    Vector3 respawnPosition;
    //Used to keep track of which scene our respawn position is saved in
    int checkPointSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
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
            FindObjectOfType<Player>().transform.position = respawnPosition;
        }
    }

    public void SetRespawnPosition(Vector3 pos) 
    {
        respawnPosition = pos;
        checkPointSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void ReloadToCheckpoint() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
