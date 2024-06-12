using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private int _exp;
    [SerializeField] private int _maxExp;
    [SerializeField] private int _maxLevel;

    public int Level => _level;
    public int Exp => _exp;
    public int MaxExp => _maxExp;
    public int MaxLevel => _maxLevel;

    public void AddExp(int exp)
    {
        _exp += exp;
        if (_exp >= _maxExp)
        {
            _exp -= _maxExp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (_level >= _maxLevel) return;
        _level++;
        // 나중에 밸런스
        _maxExp = _level * 100;
    }
}
