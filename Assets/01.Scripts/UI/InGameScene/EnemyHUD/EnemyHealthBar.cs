using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private MeshGaugeUI _meshGauge;
    private Health _owner;


    public void Initialize(Health ownerHealthCompo)
    {
        _owner = ownerHealthCompo;
        _owner.OnHealthChangedEvent.AddListener(HandleRefresh);
        HandleRefresh(1,1);
    }


    private void HandleRefresh(int currentHealth, int maxHealth)
    {
        _meshGauge.RefreshGauge(currentHealth, maxHealth);
    }

}
