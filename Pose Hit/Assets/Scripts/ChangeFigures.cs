using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFigures : MonoBehaviour
{
    [SerializeField] private GameObject[] figures;
    [SerializeField] private GameObject _tornado;
    private int _currentActiveFigure = 0;

    private void Start()
    {
        figures[_currentActiveFigure].SetActive(true);
        _tornado.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void Replace()
    {

        StartCoroutine(ReplaceCarutine());

    }

    private IEnumerator ReplaceCarutine()
    {
        float countDown = 3f;

        figures[_currentActiveFigure].SetActive(false);
        _currentActiveFigure++;

        for (int i = 0; i < 3000; i++)
        {
            while (countDown >= 0)
            {
                _tornado.SetActive(true);
                countDown -= Time.smoothDeltaTime;
                yield return null;
            }
            figures[_currentActiveFigure].SetActive(true);
            _tornado.SetActive(false);
        }
    }

}
