using System.Collections.Generic;
using System;
using UnityEngine;
public class WeaponManager : MonoSingleton<WeaponManager>
{
    public event Action<WeaponType> OnAppendWeaponEvent;
    private Dictionary<WeaponType, Weapon> _weapons;

    [SerializeField] private List<Weapon> _curWeapons; //해금된 자동발동 무기 리스트

	private void Awake()
	{
        _weapons = new Dictionary<WeaponType, Weapon>();
        _curWeapons = new List<Weapon>();
    }

	private void Start()
    {
        foreach (WeaponType weaponEnum in Enum.GetValues(typeof(WeaponType)))
        {
            if (weaponEnum == WeaponType.None) continue;
            Type t = Type.GetType($"{weaponEnum.ToString()}Weapon");
            Weapon weaponCompo = GetComponentInChildren(t) as Weapon;
            _weapons.Add(weaponEnum, weaponCompo);
            
            //초반에 활성화된 무기 추가(거의 사실 상 디버그용)
            if (weaponCompo.weaponEnabled)
                AppendWeapon(weaponEnum);
        }
    }

    public Weapon GetWeapon(WeaponType weapon)
	{
        if (weapon == WeaponType.None) return null;

        if(_weapons.TryGetValue(weapon, out Weapon weaponCompo))
		{
            
            return weaponCompo as Weapon;
        }

        return null;
    }

    [ContextMenu("DebugAppendWeapon")]
    private void AppendWeapon()
    {
        AppendWeapon(WeaponType.Mine);   
    }

    public void AppendWeapon(WeaponType weapon)
    {
        //전용 무기라면
        if(_weapons[weapon].isUniqueWeapon)
		{
            PlayerPartType playerPartType = PlayerPartController.Instance.GetCurrentPlayerPart().playerPartType;
            if(_weapons[weapon].partType != playerPartType)
			{
                //현재 파츠 타입과 같지 않다면 추가할 수 없음
                return;
			}
        }

        //이미 있다면 레벨업
        if(_curWeapons.Contains(_weapons[weapon]))
		{
            ++_weapons[weapon].level;
            return;
		}

        //전에 없다면 초기화
        _weapons[weapon].player = GameManager.Instance.Player;
        _weapons[weapon].WeaponInit();
        _weapons[weapon].weaponEnabled = true;
        _curWeapons.Add(_weapons[weapon]);
        OnAppendWeaponEvent?.Invoke(weapon);
    }

    private void Update()
    {
        foreach (var weapon in _curWeapons)
        {
            if (weapon.isConditionalWeapon) continue;
            weapon.UseWeapon();
        }
    }
}