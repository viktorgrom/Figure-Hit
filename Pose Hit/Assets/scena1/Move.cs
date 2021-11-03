using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private float _speedMove = 4f;
    

    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    
    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + Vector3.back * Time.deltaTime * _speedMove);
    }

    public void CollidePlayer()
    {
        _speedMove = 0;

        if (_speedMove < 4)
            _speedMove *= 0.1f * Time.deltaTime;
    }
}
