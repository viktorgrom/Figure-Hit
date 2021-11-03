using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    float targetAngle = 90;
    float turnSpeed = 5;
    private Vector3 currentVec;
    private bool _carutineOn = false;
    

    // Update is called once per frame
    void Update()
    {
        if (Swipes.swipeLeft  && !_carutineOn)
        {
            StartCoroutine(Rotate(Vector3.up, -90, 1.0f));
        }

        if (Swipes.swipeRight && !_carutineOn)
        {
            StartCoroutine(Rotate(Vector3.up, 90, 1.0f));
        }


    }

    private IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        _carutineOn = true;
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
        _carutineOn = false;
    }
}
