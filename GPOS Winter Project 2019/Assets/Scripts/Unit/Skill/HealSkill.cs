using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : ISkill
{
    private const int heal = 20;

    public void UseSkill()
    {
        GameObject.Find("Player").GetComponent<Unit>().Heal(20);
    }

    public void UseSkill(Unit user, Unit Target)
    {
        Target.Heal(20);
    }

    public void UseSkill(Unit user, Vector2 pos)
    {
    }
}

