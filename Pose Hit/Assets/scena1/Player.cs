using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{   
    [SerializeField] private GameObject[] _portals;    
    private float _targetSpeed = 4f;
    [SerializeField] float _currentSpeed;
    private Rigidbody _rb;  
    public int _currentActivePortal = 0;
    private float _distance;
    private bool _startGame = false;

    

    private void Start()
    {
       // SwipeManager.instance.enabled = false;
        _currentSpeed = 0;
        _rb = GetComponent<Rigidbody>();
        _portals[_currentActivePortal].SetActive(true);
    }
    

    private void FixedUpdate()
    {
        if(_rb != null)
            _rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * _currentSpeed);


        if (_currentSpeed < _targetSpeed && _startGame)
            _currentSpeed = Mathf.SmoothStep(_currentSpeed, _targetSpeed, 3 * Time.deltaTime);
    }
    
    
    private void OnTriggerExit(Collider other)
    {

        if(other.tag == "CollideCube")
        {
           
           _currentActivePortal++;
            
           _portals[_currentActivePortal].SetActive(true);


        }

        foreach (GameObject go in _portals)
        {
            if (go.activeSelf)
            {
                _distance = Vector3.Distance(go.transform.position, transform.position);
                if (_distance > 50f)
                    go.SetActive(false);
            }
        }

       
    }
    public void CollidePlayer()
    {
        _currentSpeed = 0;     
    }

    public void StartLevel()
    {
        _startGame = true;
       // SwipeManager.instance.enabled = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




    /* private Touch touch;
     private Vector2 touchPosition;
     private Quaternion rotationY;
     private float rotateSpeed = 10f;


     private void Update()
     {
         if(Input.touchCount > 0)
         {
             touch = Input.GetTouch(0);

             if(touch.phase == TouchPhase.Moved)
             {
                 //transform.rotation = (Vector3(0,90,0) * rotateSpeed)
                 transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
             }
         }
     }*/
    /* private Rigidbody rb;
     [SerializeField]
     private float _speedMove = 50f;

     private void Start()
     {
         rb = GetComponent<Rigidbody>();


     }


     private void FixedUpdate()
     {
         if (Input.GetKeyDown(KeyCode.Space))
         {
             StartCoroutine(Rotate(Vector3.up, -90, 1.0f));
         }

         rb.velocity = Vector3.up * _speedMove;
     }


     private IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
     {
         Quaternion from = transform.rotation;
         Quaternion to = transform.rotation;
         to *= Quaternion.Euler(axis * angle);

         float elapsed = 0.0f;
         while (elapsed < duration)
         {
             transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
             elapsed += Time.deltaTime;
             yield return null;
         }
         transform.rotation = to;
     }*/


}
