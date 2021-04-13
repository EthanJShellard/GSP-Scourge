using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera follow script to be placed on the main camera object. Give it the player transform in the editor and set its size for zoom (Increase size to zoom out).
/// </summary>
public class CameraFollow : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] float size;
    [SerializeField] Vector3 offset;
    [SerializeField] float lerpFactor; //Increase to make camera follow more closely/quickly

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        GetComponentInParent<Camera>().orthographicSize = size;
        transform.position = playerTransform.position;
        transform.Translate(offset);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = playerTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpFactor);

        //transform.position = playerTransform.position;
        //transform.Translate(offset);
    }

    //Assigns values when the component is first added or when reset selected in the editor
    private void Reset()
    {
        size = 5;
        offset.Set(0,2,-10);
        lerpFactor = 0.02f;
    }
}