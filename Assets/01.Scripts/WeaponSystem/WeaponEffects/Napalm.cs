using System.Collections;
using UnityEngine;

public class Napalm : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _lifeTime;
    public bool canUse;
    private DamageCaster _damageCaster;
    private ParticleSystem _explodeVFX;
    private FeedbackPlayer _soundFeedbackPlayer;
    // 
    // 

    private void Awake()
    {
        _damageCaster = GetComponent<DamageCaster>();
        _explodeVFX = GetComponentInChildren<ParticleSystem>();
        _soundFeedbackPlayer = GetComponentInChildren<FeedbackPlayer>();
    }

    public void Explode(Vector3 pos)
    {
        canUse = false;
        transform.position = pos;
        _damageCaster.CastDamage(_damage);
        _explodeVFX.Play();
        _soundFeedbackPlayer.PlayFeedback();
        StartCoroutine(ExplodeCoroutine());

    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        canUse = true;
    }



}
