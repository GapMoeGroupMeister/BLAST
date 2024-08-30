using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySkillManager<T> where T : Enemy
{
    private Dictionary<Enum, EnemySkill<T>> _skillDictionary = new Dictionary<Enum, EnemySkill<T>>();
    public bool IsUsingSkill { get; private set; } = false;
    private EnemySkill<T> _currentUsingSkill;
    public bool IsSkillUsing => _currentUsingSkill.IsUsing;

    public EnemySkillManager(T owner)
    {
        string className = typeof(T).ToString();
        Type enumType = Type.GetType($"{className}SkillEnum");
        foreach (Enum skillEnum in Enum.GetValues(enumType))
        {
            string enumName = skillEnum.ToString();
            Type t = Type.GetType($"{className}{enumName}Skill");
            Debug.Log($"{className}{enumName}Skill");
            EnemySkill<T> skill = Activator.CreateInstance(t, (object)owner) as EnemySkill<T>;
            AddSkill(skillEnum, skill);
        }
    }


    public void AddSkill(Enum skillEnum, EnemySkill<T> skill)
    {
        _skillDictionary.Add(skillEnum, skill);
    }

    public bool TryUseSkill(Enum skillEnum)
    {
        if (_skillDictionary.TryGetValue(skillEnum, out EnemySkill<T> skill))
        {
            if (skill.IsUseable())
            {
                IsUsingSkill = true;
                _currentUsingSkill = skill;
                skill.UseSkill();
                return true;
            }
        }
        return false;
    }
}
