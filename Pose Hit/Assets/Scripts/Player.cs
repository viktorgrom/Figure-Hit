using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{   
    [SerializeField] private GameObject[] _portals;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _restartBtn;

    [SerializeField] private Text _scoreTxt;
    private int _score;
    private float _waitTime = 1f;

    private float _targetSpeed = 5f;
    [SerializeField] private float _currentSpeed;
    private Rigidbody _rb;  

    private int _currentActivePortal = 0;
    private float _distance;
    private bool _startGame = false;
    public ChangeFigures changeFigures;

    private void Start()
    {
        changeFigures = FindObjectOfType<ChangeFigures>();       
        _currentSpeed = 0;
        _rb = GetComponent<Rigidbody>();
        _portals[_currentActivePortal].SetActive(true);
    }
    

    private void FixedUpdate()
    {
        //рух
        if(_rb != null)
            _rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * _currentSpeed);

        if (_currentSpeed < _targetSpeed && _startGame)
            _currentSpeed = Mathf.SmoothStep(_currentSpeed, _targetSpeed, 3 * Time.deltaTime);
    }

    //рахунок
    private IEnumerator ChangeScore()
    {
        while (true)
        {

            yield return new WaitForSeconds(_waitTime);

            if (_currentSpeed <= 1f)
            {
                _waitTime = 0.3f;
            }
            else if (_currentSpeed > 1.0f && _currentSpeed <= 4.0f)
            {
                _waitTime = -0.05f * _currentSpeed + 0.5f;
            }
            else if (_currentSpeed > 4.0f)
            {
                _waitTime = 0.1f;
            }


            if (_startGame)
            {

                _score += 10;

                _scoreTxt.text = _score.ToString();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //зіткнення
        if(other.tag == "CollideCube")
        {          
           _currentActivePortal++;           
           _portals[_currentActivePortal].SetActive(true);

            //зміна фігури
            if(_currentActivePortal == 3 || _currentActivePortal == 7)
            {
                _currentSpeed = 1;
                changeFigures.Replace();
            }
            //перемога
            if (_currentActivePortal == 10)
            {
                _winText.SetActive(true);
                _currentSpeed = 0;
                _targetSpeed = 0;
                _restartBtn.SetActive(true);
                _startGame = false;
            }
                
        }
        //очищення пам'яті від зайвих об'єктів
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
        StartCoroutine(ChangeScore());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  
}
