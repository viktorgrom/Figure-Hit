using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10f;
    private float speed = 0;
    public int maxRoadCount = 5;


    


    private void Start()
    {
        ResetLevel();
        // StartLevel();        
    }

    public void StartLevel()
    {
        speed = maxSpeed;
        SwipeManager.instance.enabled = true;
    }

    private void Update()
    {

        

       if (speed == 0)
            return;



        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if(roads[0].transform.position.z < -15)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);

            CreateNextRoad();
        }
    }

    public void ResetLevel()
    {
        speed = 0;
        while( roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }

        for(int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
        SwipeManager.instance.enabled = false;
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;

        if(roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 15);
        }

        GameObject go = Instantiate(roadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }
}
