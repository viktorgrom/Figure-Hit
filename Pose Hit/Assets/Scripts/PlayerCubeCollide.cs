using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeCollide : MonoBehaviour
{
    private Rigidbody _rb;
    private float _force = 500f;

    public Player _player;
    

    private void Start()
    {

        _player = FindObjectOfType<Player>();
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.useGravity = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {          
            _rb.isKinematic = false;
            _rb.useGravity = true;
            _rb.AddForce(Vector3.forward * _force);
           
            _player.CollidePlayer();
            print(other.name);
        }
    }


}
