using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorials;
    private float tutorialTimer;
    // Start is called before the first frame update
    void Start()
    {
        tutorials.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tutorial")
        {
            tutorials.SetActive(true);
            tutorialTimer = 100.0f;
            tutorialTimer -= Time.deltaTime;

            if(tutorialTimer <=0)
            {
                tutorials.SetActive(false);
            }
        }
    }
}
