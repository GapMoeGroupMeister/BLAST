using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="SO/StageInfo/Info")]
public class StageInfoSO : ScriptableObject
{
    public int id;
    public string stageName;
    public string stageDescription;
    public bool isLocked;
    public Color stageColor;
    public Sprite stageImageSprite;
    public StageDifficulty stageDifficulty;
    public StageWaveSO stageWaves;

    public Vector3 startPos;


}
