using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//SUMMARY
// This is a simple menu controller which allows the scene to be changed depending on the button pressed.
//END OF SUMMARY

public class MenuControls : MonoBehaviour
{
    public bool isStart;
    public bool iaQuit;
    // Start is called before the first frame update
    void OnMouseDown(string sceneName)
    {
        if (isStart)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
