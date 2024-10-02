using UnityEngine;

public class BossManager : MonoSingleton<BossManager>
{
    [SerializeField] private Enemy _currentBoss;

    public void SpawnBoss(WaveBoss bossInfo)
    {
        _currentBoss = Instantiate(bossInfo.bossPrefab, bossInfo.generatePosition, Quaternion.identity);

    }

}
