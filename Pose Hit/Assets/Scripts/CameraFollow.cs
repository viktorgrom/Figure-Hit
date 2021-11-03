using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _player;
    public Vector3 offset;
    public float _smoothSpeed = 0.0125f;
    Vector3 moveDirection;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void FixedUpdate()
    {
        Vector3 smooth = Vector3.Lerp(transform.position, _player.transform.position, _smoothSpeed);
        transform.position = smooth + offset;
    }
}
