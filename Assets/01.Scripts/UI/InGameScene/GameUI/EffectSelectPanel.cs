using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSelectPanel : UIPanel
{
    [SerializeField] private EffectSelectSlot[] slots;

    public override void Open()
    {
        base.Open();
        
    }

    public void RefreshSlots()
    {
        // Slot들에 강화시킬 파워업 카드들을 할당해줌
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetPowerUp();
        }
    }
}
