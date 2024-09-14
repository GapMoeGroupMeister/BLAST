using UnityEngine;

public class EnemyHUD : MonoBehaviour
{
    public Agent owner;
    [SerializeField] private EnemyHealthBar _enemyHealthBar;
    [SerializeField] private EnemyEffectStateUI _enemyEffectUI;

    private void Awake(){
        if(owner == null)
            owner = transform.parent.GetComponent<Agent>();

    }

    private void Start(){

        _enemyHealthBar.Initialize(owner.HealthCompo);
    }

    private void FixedUpdate(){
        SetDirection();
    }

    private void SetDirection(){
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }



}
