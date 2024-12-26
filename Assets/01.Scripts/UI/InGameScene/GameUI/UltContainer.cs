using UnityEngine;
using UnityEngine.UI;

public class UltContainer : MonoBehaviour
{
    [SerializeField] private Slider _ultGauge;
    [SerializeField] private Image _ultCompleteUI;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;
    
    private WeaponManager _weaponManager;
    private UltWeapon _currentUltWeapon;
    
    private void Start()
    {
        _currentUltWeapon = WeaponManager.Instance.GetCurrentUltWeapon();
        
        if(_currentUltWeapon == null)
            Debug.LogWarning("Current Ult Weapon is null!");
        
        //이벤트 구독
        _currentUltWeapon.OnCooldownEvent += HandleOnUltGauge;
    }

    private void OnDestroy()
    {
        _currentUltWeapon.OnCooldownEvent -= HandleOnUltGauge;
    }

    private void HandleOnUltGauge(float current, float total)
    {
        _ultGauge.value = 1 - current / total;
        if (_ultGauge.value >= 1)
        {
            _ultCompleteUI.color = _activeColor;
        }
        else
        {
            _ultCompleteUI.color = _inactiveColor;
        }
    }
}
