using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoSingleton<WeaponManager>
{
    public event Action<WeaponType> OnAppendWeaponEvent;
    private Dictionary<WeaponType, Weapon> _weapons;

    [SerializeField] private List<Weapon> _curWeapons; //해금된 자동발동 무기 리스트
    private UltWeapon _curUltWeapon;
    
    protected override void Awake()
	{
        base.Awake();
        _weapons = new Dictionary<WeaponType, Weapon>();
        _curWeapons = new List<Weapon>();
    }
    
    private void Start()
    {
        _curUltWeapon = GetComponentsInChildren<UltWeapon>().FirstOrDefault(x => 
            x.partType == PlayerPartController.GetCurrentPlayerPart().playerPartType);
        
        foreach (WeaponType weaponEnum in Enum.GetValues(typeof(WeaponType)))
        {
            if (weaponEnum == WeaponType.None) continue;
            Type t = Type.GetType($"{weaponEnum.ToString()}Weapon");
            Weapon weaponCompo = GetComponentInChildren(t) as Weapon;
            _weapons.Add(weaponEnum, weaponCompo);

            CheckCanUseForWeapon(weaponEnum);

            //초반에 활성화된 무기 추가(거의 사실 상 디버그용)
            if (weaponCompo.weaponEnabled)
                AppendWeapon(weaponEnum);
        }
        
        _curWeapons.Add(_curUltWeapon);
    }

    public int GetCurWeaponCount() => _curWeapons.Count;

    private void CheckCanUseForWeapon(WeaponType weaponType)
	{
		//해금이 안되면 false
        if(GameDataManager.Instance.TryGetWeapon(weaponType, out WeaponSave weaponSave))
		{
            Weapon weaponCompo = GetWeapon(weaponType);

            weaponCompo.canUse = true;

            //사용허가가 있으면 사용할 수 이씀
            weaponCompo.canUse = weaponSave.enabled;

            //고유 무기인데 현재 파츠와 타입이 불일치하면 false
            if (weaponCompo.isUniqueWeapon)
                weaponCompo.canUse = PlayerPartController.GetCurrentPlayerPart().playerPartType == weaponCompo.partType && weaponCompo.canUse;
		}
	}

    //isEnable를 활성화해서 현재 장착되어 있는 Weapon만 검색할 수 있음
    public Weapon GetWeapon(WeaponType weapon, bool isEnable = false)
	{
        if (weapon == WeaponType.None) return null;

        if(_weapons.TryGetValue(weapon, out Weapon weaponCompo))
		{
            //현재 활성화된 Weapon을 찾는다면 _curWeapon에 포함이 되어있는지 확인ㄱㄱ
            if (isEnable && _curWeapons.Contains(weaponCompo) == false) return null;
            return weaponCompo as Weapon;
        }

        return null;
    }

    public UltWeapon GetCurrentUltWeapon() => _curUltWeapon;
    
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
            PlayerPartType playerPartType = PlayerPartController.GetCurrentPlayerPart().playerPartType;
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
        AppendWeaponImmediately(weapon);
    }

    private void AppendWeaponImmediately(WeaponType weapon)
    {
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