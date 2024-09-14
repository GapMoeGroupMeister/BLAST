
using EffectSystem;
using UnityEngine;
using UnityEngine.UI;
public class EnemyEffectSlot : MonoBehaviour
{
    [SerializeField] private Image _iconImage;

    private EffectState _effect;
    private RectTransform _rectTrm;
    public bool isActive;

    private void Awake()
    {

    }
    public void Initialize(EffectStateSlotUIDataSO uiData, EffectState effect)
    {
        isActive = true;
        gameObject.SetActive(true);

        _effect = effect;

        _iconImage.sprite = uiData.icon;
        _iconImage.color = uiData.iconColor;
        //_effect.OnUpdateEvent += HandleRefresh; // 필요없음
        _effect.OnOverEvent += HandleDestroy;
    }

   

    private void HandleDestroy()
    {
        _effect.OnOverEvent -= HandleDestroy;
        isActive = false;
        gameObject.SetActive(false);
    }

}