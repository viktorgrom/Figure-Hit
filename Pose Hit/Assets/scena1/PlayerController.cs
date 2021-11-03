using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public Camera followCamera;

    private Rigidbody _rb;
    private Vector3 _cameraPos;
    private float _SpeedModifier = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraPos = followCamera.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 playerPos = _rb.position;
        Vector3 movement = new Vector3(0, 0, verticalInput).normalized;

        if(movement == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movement);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            _rb.MoveRotation(targetRotation);
        }
            
            
        _rb.MovePosition(playerPos + movement * _SpeedModifier * speed * Time.fixedDeltaTime);
        //
        
    }
}
