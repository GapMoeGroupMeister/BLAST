using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WaveSO")]
public class WaveSO : ScriptableObject
{
    public List<WaveEnemy> waveEnemies;
    [Header("Boss Setting")]
    public WaveBoss boss;

    [Header("Supply Setting")]

    public int supplyAmount;
    public int dropItemAmount;

    
}