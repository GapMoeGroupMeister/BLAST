using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;
    private void AnimationEnd()
    {
        _enemy.AnimationEndTrigger();
    }
}
