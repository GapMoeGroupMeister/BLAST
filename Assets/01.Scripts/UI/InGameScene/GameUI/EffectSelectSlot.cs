using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EffectSelectSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _powerUpNameText;
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

        // PowerSO를 받아서 등록 해야함    
        _icon.sprite = uiData.icon;
        _powerUpNameText.text = uiData.weaponName;
    }
}