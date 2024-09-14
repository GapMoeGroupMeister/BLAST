using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _gaugeImage;
    private Health _owner;


    public void Initialize(Health ownerHealthCompo)
    {
        _owner = ownerHealthCompo;
        _owner.OnHealthChangedEvent.AddListener(HandleRefresh);
        HandleRefresh(1,1);
    }


    private void HandleRefresh(int currentHealth, int maxHealth)
    {
        _gaugeImage.fillAmount = (float)currentHealth / maxHealth;
    }

}
