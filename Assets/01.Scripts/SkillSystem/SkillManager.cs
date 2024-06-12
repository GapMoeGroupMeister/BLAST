using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkill
{
    None = 0, 
}

public class SkillManager : MonoSingleton<SkillManager>
{
    private Dictionary<Type, Skill> _skills;

    private List<Skill> _passiveSkills; //�رݵ� �ڵ��ߵ� ��ų ����Ʈ
    
    private void Awake()
    {
        _skills = new Dictionary<Type, Skill>();
        _passiveSkills = new List<Skill>();
        foreach (PlayerSkill skillEnum in Enum.GetValues(typeof(PlayerSkill)))
        {
            if(skillEnum == PlayerSkill.None) continue;

            Skill skillCompo = GetComponent($"{skillEnum.ToString()}Skill") as Skill;
            Type type = skillCompo.GetType();
            _skills.Add(type, skillCompo);
        }
    }

    public T GetSkill<T>() where T : Skill
    {
        Type t = typeof(T); //Ÿ���� ������
        if (_skills.TryGetValue(t, out Skill target))
        {
            return target as T;
        }
        return null;
    }
    
    public Skill GetSkill(PlayerSkill skill)
    {
        Type t = Type.GetType($"{skill.ToString()}");

        if (t == null) return null;

        if (_skills.TryGetValue(t, out Skill target))
        {
            return target as Skill;
        }

        return null;
    }

    public void AddPassiveSkill(Skill skill)
    {
        _passiveSkills.Add(skill);
    }

    private void Update()
    {
        foreach (var skill in _passiveSkills)
        {
            skill.UseSkill();
        }
    }
}
