using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="SO/StageInfo")]
public class StageInfoSO : ScriptableObject
{
    public string stageName;
    public string stageDescription;
    public Sprite stageImageSprite;
    public StageDifficulty stageDifficulty;

    public Vector3 startPos;


}
