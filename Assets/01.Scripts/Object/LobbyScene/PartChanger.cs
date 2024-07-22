using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PartChanger : MonoBehaviour
{
    [SerializeField] private Transform _partConnecterTrm;
    [SerializeField] private Transform _tongsTrm;
    [SerializeField] private TongController[] _tongs;
    [SerializeField] private Transform _partGenerateTrm;
    [Space(10f)] 
    [SerializeField] private Transform _currentPartTrm;
    
    [Header("Setting Values")]
    [SerializeField] private float _upDuration;
    [SerializeField] private float _upDistance = 11f;

    private bool _isChanging;
    private Sequence _seq;

    [FormerlySerializedAs("_TestSO")] [SerializeField] private PlayerPartDataSO _testSO;
    [ContextMenu("DebugChange")]
    private void DebugChangePart()
    {
        ChangePart(_testSO);
    }
    
    public void ChangePart(PlayerPartDataSO data)
    {
        if(_isChanging) return;
        _isChanging = true;

        _seq = DOTween.Sequence();
        _seq.Append(_tongsTrm.DOMoveY(0f, _upDuration)).SetEase(Ease.InQuart);
        _currentPartTrm.SetParent(_tongsTrm);
        _seq.AppendInterval(0.3f);
        _seq.Append(_tongsTrm.DOMoveY(_upDistance, _upDuration).SetEase(Ease.OutQuart));
        _seq.AppendCallback(() =>
        {
            Destroy(_currentPartTrm.gameObject);
            _currentPartTrm = Instantiate(data.partPrefab, _tongsTrm).transform;
            // 생성이 먼저되어버려서 enable에서 말이 많다
            Destroy(_currentPartTrm.gameObject.GetComponent<PlayerPart>());
            _currentPartTrm.position = _partGenerateTrm.position;
        });
        _seq.Append(_tongsTrm.DOMoveY(0f, _upDuration).SetEase(Ease.InQuart));
        _seq.AppendCallback(() =>
        {
            _currentPartTrm.SetParent(_partConnecterTrm);
        });
        _seq.AppendInterval(0.2f);
        _seq.Append(_tongsTrm.DOMoveY(_upDistance, _upDuration));
    }

    private IEnumerator ChangeCoroutine()
    {
        
        yield return null;
    }
}
