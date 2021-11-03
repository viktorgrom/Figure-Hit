using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{   
    [SerializeField] private GameObject[] _portals;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _restartBtn;
    private float _targetSpeed = 5f;
    [SerializeField] private float _currentSpeed;
    private Rigidbody _rb;  
    private int _currentActivePortal = 0;
    private float _distance;
    private bool _startGame = false;
    public ChangeFigures _changeFigures;



    private void Start()
    {
        _changeFigures = FindObjectOfType<ChangeFigures>();       
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

            if(_currentActivePortal == 3 || _currentActivePortal == 7)
            {
                _currentSpeed = 1;
                _changeFigures.Replace();
            }

            if (_currentActivePortal == 10)
            {
                _winText.SetActive(true);
                _currentSpeed = 0;
                _targetSpeed = 0;
            }
                
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
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  
}
