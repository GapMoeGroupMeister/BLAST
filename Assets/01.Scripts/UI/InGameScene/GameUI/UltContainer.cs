using UnityEngine;
using UnityEngine.UI;

public class UltContainer : MonoBehaviour
{
    [SerializeField] private Slider _ultGauge;
    [SerializeField] private Image _ultCompleteUI;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;
    
    private WeaponManager _weaponManager;
    private UniqueWeapon _currentUniqueWeapon;
    
    private void Start()
    {
        _currentUniqueWeapon = WeaponManager.Instance.GetCurrentUltWeapon();
        
        if(_currentUniqueWeapon == null)
            Debug.LogWarning("Current Ult Weapon is null!");
        
        //이벤트 구독
        _currentUniqueWeapon.OnCooldownEvent += HandleOnUniqueGauge;
    }

    private void OnDestroy()
    {
        _currentUniqueWeapon.OnCooldownEvent -= HandleOnUniqueGauge;
    }

    private void HandleOnUniqueGauge(float current, float total)
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
