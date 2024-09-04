using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectSelectSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _powerUpNameText;
    
    
    public void SetWeaponInfo(Weapon weapon, WeaponUIData uiData)
    {
        // PowerSO를 받아서 등록 해야함    
        _icon.sprite = uiData.icon;
        _powerUpNameText.text = uiData.weaponName;

    }
}