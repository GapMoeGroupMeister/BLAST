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
            AcidicArea acidicArea = Instantiate(_acidicArea, player.transform.position, Quaternion.identity);
            acidicArea.SpawnAcidicArea();
        }
        return true;
    }
}
