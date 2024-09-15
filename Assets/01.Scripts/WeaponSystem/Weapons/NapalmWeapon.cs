using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NapalmWeapon : Weapon
{
    [SerializeField] private Napalm _napalmPrefab;
    private List<Napalm> _napalms = new List<Napalm>();
    [SerializeField] private float _shootTerm = 0.7f;
    private float _detectRange = 50f;
    private int maxColliderAmount = 10;
    private Collider[] _targets;
    private int _targetAmount;


    private void Awake(){
        _targets = new Collider[maxColliderAmount];
    }

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            StartCoroutine(ShootCoroutine());
        }	

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator ShootCoroutine()
    {
        DetectEnemy();
        for(int i = 0; i < level; i++)
        {
            Vector3 firePos = Vector3.zero;            
            if(i >= _targetAmount){ // 주변에 있는 적이 가능 공격 수보다 적을때.
                firePos = Random.insideUnitCircle.normalized * 4f; // 걍 랜덤한 위치에 갈김
                firePos.y = 0f;
            }else{
                firePos = _targets[i].transform.position;
            }

            GenerateNapalm(firePos);    
            yield return new WaitForSeconds(_shootTerm);
        }

    }

    private void GenerateNapalm(Vector3 position)
    {
        Napalm bomb = null;
        for (int i = 0; i < _napalms.Count; i++)
        {
            if(_napalms[i].canUse){
                bomb = _napalms[i];
                break;
            }
        }
        if(bomb == null){
            print("없어서 생성");
            bomb = Instantiate(_napalmPrefab, transform);
            _napalms.Add(bomb);

        }
        bomb.Explode(position);
    }


    private void DetectEnemy(){
        Vector3 position = player.transform.position;
        _targetAmount = Physics.OverlapSphereNonAlloc
            (position, _detectRange, _targets, whatIsEnemy);
          
    }
}
