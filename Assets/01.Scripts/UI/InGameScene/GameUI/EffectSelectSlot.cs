using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EffectSelectSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    private Button _button;
    public event Action OnSelectedEndEvent;
    private WeaponType _currentWeaponType;

    //Managements
    private WeaponManager _weaponManager;

    private void Awake()
	{
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleSelectCard);
    }

	private void Start()
	{
        _weaponManager = WeaponManager.Instance;
    }

	private void HandleSelectCard()
    {
        _weaponManager.AppendWeapon(_currentWeaponType);
        OnSelectedEndEvent?.Invoke();
        _button.interactable = false;
    }

    public void SetWeaponInfo(WeaponType weaponType, WeaponUIData uiData)
    {
        _button.interactable = true;
        _currentWeaponType = weaponType;

        Weapon curWeapon = _weaponManager.GetWeapon(weaponType);

        // PowerSO를 받아서 등록 해야함    
        _icon.sprite = uiData.icon;
        _skillNameText.text = uiData.weaponName;
        if (curWeapon.weaponEnabled) //이미 가지고 있으면
            _levelText.text = $"level {curWeapon.level.ToString()} > level {(curWeapon.level + 1).ToString()}";
        else
            _levelText.text = "New";
        _descriptionText.text = uiData.description;
    }
}