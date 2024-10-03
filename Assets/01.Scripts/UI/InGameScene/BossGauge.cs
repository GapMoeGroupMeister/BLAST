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
        owner.HealthCompo.OnHealthChangedEvent.AddListener(HandleGaugeRefresh);
        owner.HealthCompo.OnDieEvent.AddListener(DestroyGauge);
    }

    private void HandleGaugeRefresh(int current, int max)
    {
        float ratio = (float)current / max;
        _gaugeImage.fillAmount = ratio;
        _subGaugeImage.fillAmount = ratio;
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
        owner.HealthCompo.OnHealthChangedEvent.RemoveListener(HandleGaugeRefresh);
        owner.HealthCompo.OnDieEvent.RemoveListener(DestroyGauge);

    }
}
