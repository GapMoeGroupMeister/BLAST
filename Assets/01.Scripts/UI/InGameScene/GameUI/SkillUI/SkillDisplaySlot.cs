using UnityEngine;
using UnityEngine.UI;

public class SkillDisplaySlot : MonoBehaviour
{
    [SerializeField] private Image _gaugeFill;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Color _gaugeStartColor;
    [SerializeField] private Color _gaugeEndColor;
    private CanvasGroup _lockPanelGroup;
    public bool isActive;
    private Weapon _ownerWeapon;
    private WeaponUIData _uiData;

    private void Awake()
    {
        _lockPanelGroup = transform.Find("LockPanel").GetComponent<CanvasGroup>();
    }

    public void Initialize()
    {
        _lockPanelGroup.alpha = 1f;
    }

    public void Active(Weapon weapon, WeaponUIData uiData)
    {
        isActive = true;
        _ownerWeapon = weapon;
        _uiData = uiData;
        _iconImage.sprite = uiData.icon;
        _lockPanelGroup.alpha = 0f;
        weapon.OnCooldownEvent += HandleRefreshCoolTimeGauge;
    }

    public void HandleRefreshCoolTimeGauge(float current, float maxCooltime)
    {
        float fill = current / maxCooltime;
        _gaugeFill.color = Color.Lerp(_gaugeStartColor, _gaugeEndColor, fill);

        _gaugeFill.fillAmount = fill;
    }
}
