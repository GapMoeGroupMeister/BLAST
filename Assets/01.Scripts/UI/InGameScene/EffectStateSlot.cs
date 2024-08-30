using EffectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectStateSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _iconImage;
    private EffectState _effect;

    public void Initialize(EffectState effect)
    {
        _effect = effect;
        _effect.OnUpdateEvent += HandleRefresh;
    }
    
    public void HandleRefresh(int duration, int level)
    {
        _timeText.text = duration.ToString();
        
    }
}