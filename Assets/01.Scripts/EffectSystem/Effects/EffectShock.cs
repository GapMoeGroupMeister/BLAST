using System.Collections;
using Crogen.CrogenPooling;
using UnityEngine;

namespace EffectSystem
{
    public class EffectShock : EffectState
    {
        private readonly int _maxChainLinkAmount = 3;
        private readonly float _targetDetectRange = 20f;
        private readonly Collider[] _targets;
        private readonly LayerMask _enemyLayer;
        private bool _canChain;
        private bool _isChained;

        public EffectShock(Agent agent, bool isResist) : base(agent, isResist)
        {
            _targets = new Collider[10];
            _enemyLayer = LayerMask.GetMask("Enemy");
        }

        public override void Start(int level = 1, float duration = 10, float percent = 1f)
        {
            if (enabled)
            {
                if (_canChain)
                {
                    _canChain = false;
                    ChainShock();
                }
            }

            base.Start(level, duration, percent);
        }

        public override void UpdateBySecond()
        {
            if (WeaponManager.Instance.GetWeapon(WeaponType.ChainingLightning).enabled)
            {
                _canChain = true;

            }
            else
            {
                _ownerHealth.TakeDamage(level);
                PopupTextManager.Instance.GenerateDamagePopup(_ownerTrm.position, level, EffectStateTypeEnum.Shock, true);
            }
        }

        private void ChainShock()
        {
            int damage = (int)(level*1.5f);
            _ownerHealth.TakeDamage(damage);
            PopupTextManager.Instance.GenerateDamagePopup(_ownerTrm.position, damage, EffectStateTypeEnum.Shock, true);
            _owner.StartCoroutine(ChainShockCoroutine());
        }

        private IEnumerator ChainShockCoroutine()
        {
            yield return new WaitForSeconds(0.3f);
            int amount = Physics.OverlapSphereNonAlloc(_ownerTrm.position, _targetDetectRange, _targets, _enemyLayer);
            int chained = 0;
            for (int i = 0; i < amount; i++)
            {
                if (chained >= _maxChainLinkAmount) // 맥스 체이닝에 도달했으면 취소
                    break;

                if (_targets[i].TryGetComponent(out AgentEffectController effectController))
                {
                    EffectShock shock = effectController.GetEffectState(EffectStateTypeEnum.Shock) as EffectShock;
                    // effectController에서 GetEffectState해서 shock를 가져오고
                    // chain가능 여부, shock enable 여부를 따져보고 나서 아래를 실행
                    if (!shock._canChain)
                        continue;

                    if (shock.enabled)
                    {
                        chained++;
                        EnergySphereLaser laser = _owner.gameObject.Pop(EffectPoolType.EnergySphereLaserEffect, _ownerTrm.position + Vector3.up * 3, Quaternion.identity) as EnergySphereLaser;
                        laser.Init(_targets[i].transform, 1);
                        effectController.ApplyEffect(EffectStateTypeEnum.Shock, 1f, 1);
                        yield return new WaitForSeconds(0.2f);
                        laser.Push();

                    }
                }

            }
        }
    }
}