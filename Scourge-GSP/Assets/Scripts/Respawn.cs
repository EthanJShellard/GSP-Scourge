//Simple bit of code that allows the game to add checkpoints and take away the ones that are no longer needed. 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Respawn : MonoBehaviour
{
    public bool isActive = false;

    public static List<GameObject> RPList;

    public static Vector3 GetActiveRP()
    {
        Vector3 result = new Vector3(0, 0, 0);

        if (RPList != null)
        {
            foreach(GameObject rp in RPList)
            {
                if(rp.GetComponent<Respawn>().isActive)
                {
                    result = rp.transform.position;
                    break;
                }
            }
        }

        return result;
    }

    private void ActiveRP()
    {
        foreach (GameObject rp in RPList)
        {
            rp.GetComponent<Respawn>().isActive = false;
        }

        isActive = true;
    }

    void Start()
    {
        RPList = GameObject.FindGameObjectsWithTag("Respawn").ToList();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActiveRP();
        }
    }
}
