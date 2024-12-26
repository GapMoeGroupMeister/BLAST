using System.Collections;
using Crogen.CrogenPooling;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TTUltWeapon : UltWeapon
{
    private Player _player;
    [SerializeField] private int _count = 100;
    private Transform _firePoint;
    
    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    protected override void Start()
    {
        base.Start();
        _firePoint = PlayerPartController.GetCurrentPlayerPart().transform.Find("UltFirePoint");
    }

    protected override void UseUltWeapon()
    {
        base.UseUltWeapon();

        StartCoroutine(CoroutineFireMissiles());
    }

    private IEnumerator CoroutineFireMissiles()
    {

        for (int i = 0; i < _count; i++)
        {
            yield return new WaitForSeconds(0.02f);
            MissileMine missileMine = gameObject.Pop(WeaponEffectPoolType.MissileMineWeapon, _firePoint.position, Quaternion.identity) as MissileMine;
            missileMine.Init(level, this);
            CameraShakeController.Instance.ShakeCam(1, 0.02f);
            float randRadAngle = Mathf.Deg2Rad * Random.Range(0f, 360f) * i;
            Vector3 dir = new Vector3(Mathf.Cos(randRadAngle), 0, Mathf.Sin(randRadAngle));
            missileMine.SetDirection(dir);
        }
    }
}
