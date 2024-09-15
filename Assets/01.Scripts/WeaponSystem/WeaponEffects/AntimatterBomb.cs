using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntimatterBomb : MonoBehaviour
{
    [SerializeField] private float _downTime = 1f;
    [SerializeField] private float _maxScale = 25f;
    //이거도 수정해주
    [SerializeField] private int _tickDamage = 5;

    private bool _explosing = false;
    private float _prevAttackTime;
    private float _tickDelay = 0.25f;
    private Sequence _exSeq;
    private Collider[] _coll;

    private void Awake()
    {
        _coll = new Collider[5];
    }

    private void OnEnable()
    {
        _prevAttackTime = Time.time;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    Explosion();
        //}
    }

    private void FixedUpdate()
    {
        if (_explosing == false) return;

        if (_prevAttackTime + _tickDelay < Time.time)
        {
            int enemyCnt = Physics.OverlapSphereNonAlloc(transform.position, transform.localScale.x / 2, _coll);


            if (enemyCnt > 0)
            {
                for (int i = 0; i < enemyCnt; i++)
                {
                    if (_coll[i].TryGetComponent(out IDamageable enemy))
                    {
                        enemy.TakeDamage(_tickDamage);
                    }
                }
            }
            _prevAttackTime = Time.time;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (_explosing == false) return;

    //    if (other.TryGetComponent(out IDamageable enemy))
    //    {
    //        enemy.TakeDamage(_tickDamage);
    //    }
    //}

    public void Explosion()
    {
        if (_exSeq != null && _exSeq.active)
            _exSeq.Kill();

        _exSeq = DOTween.Sequence();

        _exSeq.Append(transform.DOMoveY(0.5f, _downTime).SetEase(Ease.Linear))
            .AppendInterval(0.1f)
            .AppendCallback(() =>
            {
                CameraShakeController.Instance.ShakeCam(2f, 0.15f);
                _explosing = true;
            })
            .Append(transform.DOScale(_maxScale, 0.15f).SetEase(Ease.InQuad))
            .AppendInterval(1f)
            .Append(transform.DOScale(0, 1.5f).SetEase(Ease.InSine))
            .AppendCallback(() => Destroy(gameObject));
    }
}
