using UnityEngine;

public class ExpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _expDropObject;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnExp), 1, 0.2f);
    }
    
    private void SpawnExp()
    {
        Instantiate(_expDropObject, transform.position, Quaternion.identity);
    }
}