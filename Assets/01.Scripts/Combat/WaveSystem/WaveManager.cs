using System;
using System.Collections;
using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoSingleton<WaveManager>
{
    public event Action<int> OnWaveClearEvent;
    public StageWaveSO stageWaves;
    private WaveSO[] _waveList;
    [SerializeField] private List<Transform> spawnPoints; // 나중에 이것도 스테이지 추가됨에 따라서 변경해야될 것으로 보임

    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    public int CurrentWave { get; private set; }
    private int _currentEnemyCount;

    private void Start()
    {
        // Stage관리자로 부터 WAve를 할당받음
        _waveList = stageWaves.wavelist;

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

    public void StartWave(int waveIndex, bool isRandomSpawn)
    {
        CurrentWave = waveIndex;
        StartCoroutine(SpawnEnemy(waveIndex, isRandomSpawn));
    }

    private IEnumerator SpawnEnemy(int wave, bool isRandomSpawn)
    {
        if (wave >= _waveList.Length) yield break;

        int enemyIdx = 0;
        WaveSO waveSO = _waveList[wave];
        bool isBossWave = (waveSO.boss.bossPrefab != null);
        if (waveSO == null)
        {
            Debug.LogError($"Wave {wave} is null");
            yield break;
        }
        if (isBossWave)
        {
            UIManager.Instance.Open(InGameUIEnum.BossWarning);
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
        // Wave설정에  보스가 있으면 소환
        if (isBossWave)
        {
            // 보스 대충 소환해주는 코드
            BossManager.Instance.SpawnBoss(waveSO.boss);
        }
        OnWaveClearEvent?.Invoke(wave);
        _currentEnemyCount = 0;
        yield return new WaitForSeconds(stageWaves.waveTerm);
        print("다음 웨이브");
        StartWave(CurrentWave + 1, isRandomSpawn);
    }

    private IEnumerator SpawnEnemy(WaveEnemy waveEnemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Enemy enemy =
            gameObject.Pop(waveEnemy.enemyType, spawnPoint.position, Quaternion.identity) as Enemy;
        // Level 추가시 여기서 설정 해야디

        _spawnedEnemies.Add(enemy);
        _currentEnemyCount++;
        yield return new WaitForSeconds(waveEnemy.spawnDelay);
    }

    private int AllEnemyCount(int wave)
    {
        int count = 0;
        foreach (var enemy in _waveList[wave].waveEnemies)
        {
            count += enemy.enemyAmount;
        }
        return count;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (_spawnedEnemies.Contains(enemy))
        {
            _spawnedEnemies.Remove(enemy);
            _currentEnemyCount--;
        }
    }
}
