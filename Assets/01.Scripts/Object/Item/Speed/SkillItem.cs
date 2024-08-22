using System;
using ItemManage;
using UnityEngine;

public class SkillItem : Item
{
    private void Start()
    {
        OnInteractEvent += HandleSkillAdd;
    }

    private void HandleSkillAdd()
    {
        
    }
}