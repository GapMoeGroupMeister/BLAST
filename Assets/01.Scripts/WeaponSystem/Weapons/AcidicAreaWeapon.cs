using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AcidicAreaWeapon : Weapon
{
    [SerializeField] private AcidicArea _acidicArea;
    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
            AcidicArea acidicArea = Instantiate(_acidicArea, player.transform.position + new Vector3(0,1,0) , Quaternion.identity);
            acidicArea.SpawnAcidicArea();
            Destroy(acidicArea.gameObject, acidicArea.AcidicAreaDuration);
        }
        return true;
    }
}
