using Crogen.CrogenPooling;
using EffectSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class PopupTextManager : MonoSingleton<PopupTextManager>
{
    [Header("Color Setting")]
    [SerializeField] private Color _burnColor;
    [SerializeField] private Color _shockColor;

    [ContextMenu("DebugPop")]
    private void DebugPopUp()
    {
        GenerateDamagePopup(Vector3.zero, Random.Range(9, 13), EffectStateTypeEnum.Burn, true);
    }
    
    public void GenerateDamagePopup(Vector3 origin, int damage, EffectStateTypeEnum effectType, bool isCritical)
    {
        TextPopUp popUp = gameObject.Pop(OtherPoolType.TextPopup, origin, Quaternion.identity) as TextPopUp;
        Color color = Color.white;
        switch (effectType)
        {
            case EffectStateTypeEnum.Burn:
                color = _burnColor;
                break;
            case EffectStateTypeEnum.Shock:
                color = _shockColor;
                break;
        }
        
        popUp.Initialize(origin + Random.insideUnitSphere.normalized, damage.ToString(), color, isCritical);
    }
}