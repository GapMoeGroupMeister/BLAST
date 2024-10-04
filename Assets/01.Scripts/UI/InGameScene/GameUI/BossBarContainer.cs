using UnityEngine;

public class BossBarContainer : MonoBehaviour
{
    [SerializeField] private BossGauge _gaugeBarPrefab;

    public void GenerateBossBar(Enemy enemy) {

        BossGauge gauge = Instantiate(_gaugeBarPrefab, transform);
        gauge.Initialize(enemy);

    }   

}
