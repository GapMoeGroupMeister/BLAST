using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplaySlot : MonoBehaviour
{
    [SerializeField] private Image _gaugeFill;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Color _gaugeStartColor;
    [SerializeField] private Color _gaugeEndColor;

    private Skill _ownerSkill;
    
    public void Initialize(Skill skill)
    {
        _ownerSkill = skill;
    }

    public void HandleRefreshCoolTimeGauge(float fill)
    {
        _gaugeFill.color = Color.Lerp(_gaugeStartColor, _gaugeEndColor, fill);

        _gaugeFill.fillAmount = fill;
    }
}
