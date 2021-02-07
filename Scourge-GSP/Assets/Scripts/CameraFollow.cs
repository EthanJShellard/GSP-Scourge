using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera follow script to be placed on the main camera object. Give it the player transform in the editor and set its size for zoom (Increase size to zoom out).
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float size;
    public float zPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerTransform.position;
        transform.Translate(0, 0, zPos);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
        transform.Translate(0, 0, zPos);
    }
}