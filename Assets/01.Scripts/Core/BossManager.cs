using Crogen.CrogenPooling;
using UnityEngine;

public class BossManager : MonoSingleton<BossManager>
{
    [SerializeField] private BossBarContainer _bossBarContainer;
    [SerializeField] private Enemy _currentBoss;
    private Boss _boss;

    public Boss SpawnBoss(WaveBoss bossInfo)
    {
        _currentBoss = gameObject.Pop(bossInfo.bossPoolType, bossInfo.generatePosition, Quaternion.identity) as Boss;
        _boss = (Boss)_currentBoss;
        _bossBarContainer.GenerateBossBar(_currentBoss);
        return _boss;
    }

}
