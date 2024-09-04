using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoSingleton<GameDataManager>
{
    [SerializeField] private TechTree _tree;

    private int coin = 0;
    public int Coin => coin;

    public Action OnGatherCoin;
    public Action OnUseCoin;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }



    #region Coin

    /// <summary>
    /// ���� ������ �Ծ��� �� �߱���� ȿ���� �� ���� ���ݾ�?
    /// </summary>
    /// <param name="value"></param>
    public void AddCoin(int value)
    {
        OnGatherCoin?.Invoke();
        coin += value;
    }

    /// <summary>
    /// ���� ������ �Ҿ��� ���� ȿ���� ���� �� ���Ƽ� �ʿ������ ��
    /// </summary>
    /// <param name="value"></param>
    public void UseCoin(int value)
    {
        OnUseCoin?.Invoke();
        coin -= value;
    }

    #endregion

    #region Save&Load

    public void Save()
    {
        DataSave save = new DataSave();

        save.coin = coin;
    }

    public void Load()
    {

    }

    #endregion
}

[Serializable]
public struct DataSave
{
    public int coin;
    public List<WeaponSave> parts;
    public List<WeaponSave> weapons;
}

[Serializable]
public struct WeaponSave
{
    public int id;
    public bool enabled;
}