using UnityEngine;

[CreateAssetMenu(menuName = "SO/PartData/PlayerPartData")]
public class PlayerPartDataSO : ScriptableObject
{
    public int id;
    public string partName;
    public PlayerPart partPrefab;
    public Sprite partImage;
    public StatDataSO statData;

    [Header("Status Setting")] 
    public float damage;
    public float mobility;
    public float defence;
    public float utility;
}