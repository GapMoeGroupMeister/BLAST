using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Supplies : MonoBehaviour
{
    [SerializeField] private EffectPlayer _explosionParticle;
    [SerializeField] private float _speed = 1f;
    
    public void GetSupplies(Vector3 startPos, Vector3 position)
    {
        StartCoroutine(SuppliesEffect(startPos, position));
    }

    private IEnumerator SuppliesEffect(Vector3 startPos, Vector3 endPos)
    {
        transform.position = startPos;
        Vector3 direction = (endPos - startPos).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        while (transform.position != endPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(_explosionParticle, transform.position, Quaternion.identity);
    }
}