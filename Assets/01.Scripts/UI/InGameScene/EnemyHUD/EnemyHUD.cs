using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHUD : MonoBehaviour
{
    public Agent owner;
    [SerializeField] private EnemyHealthBar _enemyHealthBar;
    [SerializeField] private EnemyEffectStateUI _enemyEffectUI;


    private void Start(){

        _enemyHealthBar.Initialize(owner.HealthCompo);
    }



}
