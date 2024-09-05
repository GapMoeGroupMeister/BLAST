using System;
using System.Collections.Generic;
using UnityEngine;
using EasySave.Json;
using System.IO;

public class GameDataManager : MonoSingleton<GameDataManager>
{
    [SerializeField] private WeaponUIDataSO _weaponUIDataSO;
    [SerializeField] private TechTree _tree;
    private string _path = "GameData";

    private int coin = 0;
    private List<PartSave> parts;
    private List<WeaponSave> weapons;

    public int Coin => coin;

    public Action OnGatherCoin;
    public Action OnUseCoin;

    public DataSave s;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        s = default;

        Load();
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
        //�����صа� ������
        DataSave save = new DataSave();

        save.coin = coin;
        save.parts = parts;
        save.weapons = weapons;

        //�ϴ� prettyprint true�� �صΰ� ���� �Ҷ� ��
        EasyToJson.ToJson(save, _path, true);
    }

    public void Load()
    {
        string path = Path.Combine(Application.dataPath, _path, ".json");

        if (!File.Exists(path))
        {

        }

        DataSave save = EasyToJson.FromJson<DataSave>(_path);

        coin = save.coin;
        parts = save.parts;
        weapons = save.weapons;
    }

    public void EnablePart(PlayerPartType openPart)
    {
        parts.ForEach((part) =>
        {
            if (part.id == (int)openPart)
                part.enabled = true;
        });

        Save();
    }

    public void EnableWeapon(WeaponType openWeapon)
    {
        weapons.ForEach((weapon) =>
        {
            if (weapon.id == (int)openWeapon)
            {
                weapon.enabled = true;
                if (_weaponUIDataSO[openWeapon].isUniqueWeapon) return;

                int parentPartId = (int)_weaponUIDataSO[openWeapon].parentPart;
                parts.ForEach((part) =>
                {
                    if (part.id == parentPartId)
                        part.level++;
                });
            }
        });

        Save();
    }

    #endregion
}

[Serializable]
public class DataSave
{
    public int coin;
    public List<PartSave> parts;
    public List<WeaponSave> weapons;

    public DataSave()
    {
        coin = 0;
        parts = new List<PartSave>();
        foreach (var partEnum in Enum.GetValues(typeof(PlayerPartType)))
        {
            PartSave part = new PartSave();
            part.id = (int)partEnum;
            part.enabled = (PlayerPartType)partEnum == PlayerPartType.Default;
            part.level = 0;

            parts.Add(part);
        }

        weapons = new List<WeaponSave>();
        foreach (var weaponEnum in Enum.GetValues(typeof(WeaponType)))
        {
            WeaponSave weapon = new WeaponSave();
            weapon.id = (int)weaponEnum;
            weapon.enabled = false;

            weapons.Add(weapon);
        }
    }
}

[Serializable]
public class WeaponSave
{
    public int id;
    public bool enabled;
}

[Serializable]
public class PartSave
{
    public int id;
    public int level;
    public bool enabled;
}