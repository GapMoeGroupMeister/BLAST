using System.Collections.Generic;
using UnityEngine;

namespace BLAST.MapSystem
{
    public class MapSpawner : MonoBehaviour
    {
        [SerializeField] private Map _mapPrefab;
        private List<Map> _mapTiles = new List<Map>();
        private int _mapSize;

        private void Start()
        {
            _mapSize = _mapPrefab.MapSize;
            InitializeMapTiles();
        }

        private void Update()
        {
            UpdateMapTiles();
        }

        private void InitializeMapTiles()
        {
            Vector3 playerPos = GameManager.Instance.Player.transform.position;
            Vector3 centerPos = new Vector3(
                Mathf.Round(playerPos.x / _mapSize) * _mapSize,
                0,
                Mathf.Round(playerPos.z / _mapSize) * _mapSize);

            for (int i = 0; i < 9; i++)
            {
                Vector3 spawnPos = centerPos + new Vector3(
                    (i % 3 - 1) * _mapSize,
                    0,
                    (i / 3 - 1) * _mapSize);
                Map map = Instantiate(_mapPrefab, spawnPos, Quaternion.identity);
                map.transform.parent = transform;
                _mapTiles.Add(map);
                map.SpawnObjects();
            }
        }

        private void UpdateMapTiles()
        {
            Vector3 playerPos = GameManager.Instance.Player.transform.position;

            foreach (var tile in _mapTiles)
            {
                Vector3 tilePos = tile.transform.position;

                float distX = Mathf.Abs(playerPos.x - tilePos.x);
                float distZ = Mathf.Abs(playerPos.z - tilePos.z);

                if (distX > _mapSize * 1.5f)
                {
                    float offsetX = playerPos.x > tilePos.x ? _mapSize * 3 : -_mapSize * 3;
                    tile.transform.position += new Vector3(offsetX, 0, 0);
                }
                else if (distZ > _mapSize * 1.5f)
                {
                    float offsetZ = playerPos.z > tilePos.z ? _mapSize * 3 : -_mapSize * 3;
                    tile.transform.position += new Vector3(0, 0, offsetZ);
                }
            }
        }
    }
}
