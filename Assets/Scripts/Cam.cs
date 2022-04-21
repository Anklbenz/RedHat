using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Camera : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 3f;
    public Transform target;

    [SerializeField]
    private SpriteRenderer mapSpriteRenderer;


    void LateUpdate()
    {
        Vector3 pos = target.position;
        pos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, pos, cameraSpeed * Time.deltaTime);
    }
}
