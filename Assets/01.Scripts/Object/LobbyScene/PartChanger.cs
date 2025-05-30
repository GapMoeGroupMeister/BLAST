using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Objects.PartSelect
{

    public class PartChanger : MonoBehaviour
    {
        public event Action OnCompletePartChange;
        [SerializeField] private Transform _partConnecterTrm;
        [SerializeField] private Transform _tongsTrm;
        [SerializeField] private TongController[] _tongs;
        [SerializeField] private Transform _partGenerateTrm;
        [Space(10f)]
        [SerializeField] private Transform _currentPartTrm;

        [SerializeField] private ParticleSystem _particle;
        [Header("Setting Values")]
        [SerializeField] private float _upDuration;
        [SerializeField] private float _upDistance = 11f;

        private bool _isChanging;
        private Sequence _seq;
        [SerializeField] private PlayerPartDataSO _testSO;

        public bool IsChanging => _isChanging;
        [SerializeField] private PlayerPartDataListSO partData;

        private void Awake()
        {
            //이거 하드 코딩 나중에 수정 바람. - 2023.10.10 / 12:58 / 최영환
            int index = SaveManager.GetCurrentPlayerPart();
            _currentPartTrm = Instantiate(partData.partPairList[index].partPrefab, _tongsTrm).transform;
            _currentPartTrm.gameObject.layer = 0;
            foreach (Transform trm in _currentPartTrm)
            {
                trm.gameObject.layer = 0;
            }
            _currentPartTrm.SetParent(_partConnecterTrm);
            _currentPartTrm.localPosition = Vector3.zero;
        }


        private void Start()
        {
            PlayerCustomColorLoader.AddRenderers(_currentPartTrm);
        }

        [ContextMenu("DebugChange")]
        private void DebugChangePart()
        {
            ChangePart(_testSO);
        }

        public void ChangePart(PlayerPartDataSO data)
        {
            if (_isChanging) return;
            _isChanging = true;

            _seq = DOTween.Sequence();
            _seq.AppendCallback(() =>
            {
                _tongs[0].SetGrab(false);
                _tongs[1].SetGrab(false);
            });
            _seq.Append(_tongsTrm.DOMoveY(0f, _upDuration));
            _seq.AppendCallback(() =>
            {
                _currentPartTrm.SetParent(_tongsTrm);
            });
            _seq.AppendCallback(() =>
            {
                ShakeTongs();

                _tongs[0].SetGrab(true);
                _tongs[1].SetGrab(true);
                _particle.Play();
            });
            _seq.AppendInterval(0.8f);
            _seq.Append(_tongsTrm.DOMoveY(_upDistance, _upDuration).SetEase(Ease.InQuart));
            _seq.AppendCallback(() =>
            {
                Destroy(_currentPartTrm.gameObject);
                _currentPartTrm = Instantiate(data.partPrefab, _tongsTrm).transform;
                _currentPartTrm.gameObject.layer = 0;
                PlayerCustomColorLoader.AddRenderers(_currentPartTrm);
                PlayerCustomColorLoader.LoadAndSetColor();
                Debug.Log("Load");
                foreach (Transform child in _currentPartTrm)
                {
                    child.gameObject.layer = 0;
                }
                // 생성이 먼저되어버려서 enable에서 말이 많다
                Destroy(_currentPartTrm.gameObject.GetComponent<PlayerPart>());
                _currentPartTrm.position = _partGenerateTrm.position;
            });
            _seq.AppendInterval(0.6f);
            _seq.Append(_tongsTrm.DOMoveY(-2, _upDuration));
            _seq.AppendCallback(() =>
            {
                ShakeTongs();

                _tongs[0].SetGrab(false);
                _tongs[1].SetGrab(false);
                _particle.Play();
            });
            _seq.AppendCallback(() =>
            {

                _currentPartTrm.SetParent(_partConnecterTrm);
            });
            _seq.AppendInterval(0.4f);
            _seq.Append(_tongsTrm.DOMoveY(_upDistance, _upDuration).OnComplete(() =>
            {
                _isChanging = false;
                OnCompletePartChange?.Invoke();
            }));
        }

        private void ShakeTongs()
        {
            _tongsTrm.DOShakePosition(0.1f, 0.3f, 50);
        }

    }

}