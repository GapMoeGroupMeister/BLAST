using Crogen.ObjectPooling;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class TextPopUp : MonoBehaviour, IPoolingObject
{
    private TextMeshPro _text;
    [SerializeField] private float _moveUpYDelta = 5f;
    [SerializeField] private float _lifeTime = 0.5f;
    private Transform _moveHandleTrm;
    private Transform _textTrm;

    private void Awake()
    {
        _moveHandleTrm = transform.Find("TextMoveHandle");
        _text = _moveHandleTrm.GetComponentInChildren<TextMeshPro>();
        _textTrm = _text.transform;
    }

    public void Initialize(Vector3 position, string content, Color color, bool isBold)
    {
        transform.position = position;
        _text.text = content;
        _text.color = color;
        if (isBold)
            _text.fontStyle = FontStyles.Bold;
        else
            _text.fontStyle = FontStyles.Normal;
    }

    public PoolType OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    public void OnPop()
    {
        Sequence seq = DOTween.Sequence();
        _textTrm.localScale = Vector3.one;
        _moveHandleTrm.localPosition = Vector3.zero;
        seq.Append(_moveHandleTrm.DOLocalMoveY(_moveUpYDelta, _lifeTime));
        seq.Join(_textTrm.DOScale(0f, _lifeTime).SetEase(Ease.InQuint));
        seq.AppendCallback(() => this.Push());

    }

    public void OnPush()
    {
        
    }
}
