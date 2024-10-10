using UnityEngine;

public class BossManager : MonoSingleton<BossManager>
{
    [SerializeField] private BossBarContainer _bossBarContainer;
    [SerializeField] private Enemy _currentBoss;
    private Boss _boss;

    public Boss SpawnBoss(WaveBoss bossInfo)
    {
        _currentBoss = Instantiate(bossInfo.bossPrefab, bossInfo.generatePosition, Quaternion.identity);
        _boss = (Boss) _currentBoss;
        _bossBarContainer.GenerateBossBar(_currentBoss);
        return _boss;
    }

}
