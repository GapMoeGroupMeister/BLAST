using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Crogen.ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoSingleton<WaveManager>
{
    public event Action<int> OnWaveClearEvent;
    [SerializeField] private SerializedDictionary<int, WaveSO> waveDictionary;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float _waveDelay = 5f;
    
    private List<Enemy> _spawnedEnemies = new List<Enemy>();
    
    private int _currentWaveIndex;
    private int _currentEnemyCount;

    private void Start()
    {
        StartWave(0, true);
        
    }
    
    [ContextMenu("DebugStartWave")]
    public void DebugStartWave()
    {
        StartWave(0, false);
    }
    
    [ContextMenu("DebugStartRandomWave")]
    public void DebugStartRandomWave()
    {
        StartWave(0, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugStartRandomWave();
        }
    }

    public void StartWave(int waveIndex, bool isRandomSpawn)
    {
        _currentWaveIndex = waveIndex;
        StartCoroutine(SpawnEnemy(waveIndex, isRandomSpawn));
    }

    private IEnumerator SpawnEnemy(int wave, bool isRandomSpawn)
    {
        if (wave >= waveDictionary.Count) yield break;
        
        int enemyIdx = 0;
        WaveSO waveSO = waveDictionary[wave];
        if (waveSO == null)
        {
            Debug.LogError($"Wave {wave} is null");
            yield break;
        }
        int allEnemy = AllEnemyCount(wave);
        while (_currentEnemyCount < allEnemy)
        {
            if (!isRandomSpawn)
            {
                WaveEnemy waveEnemy = waveSO.waveEnemies[enemyIdx];
                for (int i = 0; i < waveEnemy.enemyAmount; i++)
                {
                    yield return StartCoroutine(SpawnEnemy(waveEnemy));
                }
                enemyIdx++;
            }
            else
            {
                WaveEnemy waveEnemy = waveSO.waveEnemies[Random.Range(0, waveSO.waveEnemies.Count)];
                yield return StartCoroutine(SpawnEnemy(waveEnemy));
            }
            yield return null;
        }
        
        Debug.Log($"Wave {wave} Spawn Complete");
        
        yield return new WaitUntil(() => _spawnedEnemies.Count == 0);
        OnWaveClearEvent?.Invoke(wave);
        _currentEnemyCount = 0;
        yield return new WaitForSeconds(_waveDelay);
        StartWave(_currentWaveIndex + 1, isRandomSpawn);
    }

    private IEnumerator SpawnEnemy(WaveEnemy waveEnemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Enemy enemy =
            gameObject.Pop(waveEnemy.enemyType, spawnPoint.position, Quaternion.identity) as Enemy;
        _spawnedEnemies.Add(enemy);
        _currentEnemyCount++;
        yield return new WaitForSeconds(waveEnemy.spawnDelay);
    }

    private int AllEnemyCount(int wave)
    {
        int count = 0;
        foreach (var enemy in waveDictionary[wave].waveEnemies)
        {
            count += enemy.enemyAmount;
        }
        return count;
    }
    
    public void RemoveEnemy(Enemy enemy)
    {
        _spawnedEnemies.Remove(enemy);
    }
}
