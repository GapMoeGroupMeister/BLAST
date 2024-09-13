using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace EffectSystem
{
    public class EffectShock : EffectState
    {
        private int _maxChainLinkAmount = 3;
        private float _targetDetectRange = 6f;
        private Collider[] _targets;
        private LayerMask _enemyLayer;
        public bool canChain;
        private bool _isChained;

        public EffectShock(Agent agent, bool isResist) : base(agent, isResist)
        {
            _targets = new Collider[10];
            _enemyLayer = LayerMask.GetMask("Enemy");
        }

        public override void Start(int level = 1, float duration = 10, float percent = 1f)
        {
            if(enabled)
            {
                if(canChain){
                    canChain = false;
                    ChainShock();
                }
            }
            
            base.Start(level, duration, percent);
        }

        private void ChainShock(){
            
        }

        private IEnumerator ChainShockCoroutine()
        {
            int amount = Physics.OverlapSphereNonAlloc(_ownerTrm.position, _targetDetectRange, _targets, _enemyLayer);
            int chained = 0;
            for (int i = 0; i < amount; i++)
            {
                if(chained >= _maxChainLinkAmount) // 맥스 체이닝에 도달했으면 취소
                    break;

                if(_targets[i].TryGetComponent(out AgentEffectController effectController))
                {
                    // effectController에서 GetEffectState해서 shock를 가져오고
                    // chain가능 여부, shock enable 여부를 따져보고 나서 아래를 실행
                    if(true){
                        chained++;
                        effectController.ApplyEffect(EffectStateTypeEnum.Shock, 1f, 1);
                    }
                }
                yield return new WaitForSeconds(0.2f);

            }
        }
    }
}