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
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerTransform.position;
        transform.Translate(offset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
        transform.Translate(offset);
    }

    private void Reset()
    {
        size = 5;
        offset.Set(0,2,-10);
    }
}