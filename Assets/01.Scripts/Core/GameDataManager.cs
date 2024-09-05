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
    /// 뭔가 코인을 먹었을 때 삐까뻔적한 효과를 줄 수도 있잖아?
    /// </summary>
    /// <param name="value"></param>
    public void AddCoin(int value)
    {
        OnGatherCoin?.Invoke();
        coin += value;
    }

    /// <summary>
    /// 뭔가 코인을 잃었을 때도 효과가 있을 거 같아서 필요없으면 빼
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
        //저장해둔게 없으면
        DataSave save = new DataSave();

        save.coin = coin;
        save.parts = parts;
        save.weapons = weapons;

        //일단 prettyprint true로 해두고 빌드 할때 빼
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