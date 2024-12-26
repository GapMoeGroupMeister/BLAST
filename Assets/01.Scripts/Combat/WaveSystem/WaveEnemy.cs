using Crogen.CrogenPooling;

[System.Serializable]
public struct WaveEnemy
{
    public EnemyPoolType enemyType;
    public int enemyLevel; // 높아짐에 따라서 에너미 체력이나 공격력 증가
    public int enemyAmount;
    public float spawnDelay;
}