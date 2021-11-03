using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private List<GameObject> _activeTiles = new List<GameObject>();
    private float _spawnPos = 0;
    private float _tileLength = 30;

    [SerializeField] private Transform _player;
    private int _startTiles = 6;

    void Start()
    {
        for (int i = 0; i < _startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        if (_player.position.z - 60 > _spawnPos - (_startTiles * _tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * _spawnPos, transform.rotation);
        _activeTiles.Add(nextTile);
        _spawnPos += _tileLength;
    }
    private void DeleteTile()
    {
        Destroy(_activeTiles[0]);
        _activeTiles.RemoveAt(0);
    }
}
