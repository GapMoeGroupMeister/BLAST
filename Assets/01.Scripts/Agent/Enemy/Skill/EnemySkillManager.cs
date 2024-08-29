using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySkillManager<T> where T : Enemy
{
    private Dictionary<Enum, EnemySkill<T>> _skillDictionary = new Dictionary<Enum, EnemySkill<T>>();
    public bool IsUsingSkill { get; private set; } = false;

    public EnemySkillManager(T owner)
    {
        string className = typeof(T).ToString();
        Type enumType = Type.GetType($"{className}SkillEnum");
        foreach (Enum skillEnum in Enum.GetValues(enumType))
        {
            string enumName = skillEnum.ToString();
            Type t = Type.GetType($"{className}{enumName}Skill");
            EnemySkill<T> skill = Activator.CreateInstance(t, (object)owner) as EnemySkill<T>;
            AddSkill(skillEnum, skill);
        }
    }

    public void AddSkill(Enum skillEnum, EnemySkill<T> skill)
    {
        _skillDictionary.Add(skillEnum, skill);
    }

    public void UpdateCooltime()
    {
        _skillDictionary.Values.ToList().ForEach(x => x.UpdateCooltime());
    }

    public void TryUseSkill(Enum skillEnum)
    {
        if (_skillDictionary.TryGetValue(skillEnum, out EnemySkill<T> skill))
        {
            if (skill.IsUseable)
            {
                IsUsingSkill = true;
                skill.UseSkill();
            }
        }
    }
}
