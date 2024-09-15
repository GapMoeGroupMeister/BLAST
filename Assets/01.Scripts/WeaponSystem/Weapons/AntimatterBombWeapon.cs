using UnityEngine;

public class AntimatterBombWeapon : Weapon
{
    [SerializeField] private AntimatterBomb bombPf;
    [SerializeField] private float _detectingRange = 40f;
    private Collider[] _enemyColl;

    private void Awake()
    {
        _enemyColl = new Collider[32];
    }

    public override bool UseWeapon()
    {
        if (base.UseWeapon())
        {
            int enemCnt = Physics.OverlapSphereNonAlloc(player.transform.position, _detectingRange, _enemyColl, whatIsEnemy);

            if (enemCnt > 0)
            {
                Collider closestColl = _enemyColl[0];
                for (int i = 1; i < enemCnt; i++)
                {
                    float prevDist = Vector3.Distance(player.transform.position, closestColl.transform.position);
                    float newDist = Vector3.Distance(player.transform.position, _enemyColl[i].transform.position);

                    if (newDist < prevDist) closestColl = _enemyColl[i];
                }

                Vector3 bombPosition = closestColl.transform.position;
                bombPosition.y = 15f;

                AntimatterBomb bomb = Instantiate(bombPf, bombPosition, Quaternion.identity);
                bomb.Explosion();

                return true;
            }

            return false;
        }

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (player != null)
            Gizmos.DrawWireSphere(player.transform.position, _detectingRange);
    }
}
