using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossGauge : UIPanel
{
    [SerializeField] private Image _subGaugeImage;
    [SerializeField] private Image _gaugeImage;
    [SerializeField] private Image _shieldImage;
    [SerializeField] private TextMeshProUGUI _bossNameText;
    public Agent owner;


    public void Initialize(Agent owner)
    {
        this.owner = owner;
        //owner.HealthCompo.OnHealthChangedEvent
        owner.HealthCompo.OnDieEvent.AddListener(DestroyGauge);
    }
    
    public void HandleRefresh()
    {
        
    }


    public void HandleRefreshShield()
    {
        
    }

    private void DestroyGauge()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleX(0f, 1f));
        seq.JoinCallback(() => SetCanvasActive(false));
        seq.AppendCallback(() => Destroy(gameObject));

    }
}
