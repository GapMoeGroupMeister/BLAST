using System.Collections;
using UnityEngine;

public class OilTerrorWeapon : Weapon
{
    [SerializeField] private float _oilShootTerm;
    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            //여기에 로직

            StartCoroutine(SpreadOilCoroutine());
        }	    

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator SpreadOilCoroutine()
    {
        float duration = level * 2f + 3f;
        float currentTime = 0;
        float shootCountTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            shootCountTime += Time.deltaTime;
            if (shootCountTime > _oilShootTerm)
            {
                shootCountTime = 0;
                
            }
            yield return null;
        }
    }

    private void PutOil()
    {
        
        
    }
}
