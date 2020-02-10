using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : ISkill
{
    private const uint heal = 20;

    public void UseSkill()
    {
    }

    public void UseSkill(Unit Target)
    {
        Target.Heal(20);
    }

    public void UseSkill(Vector2 pos)
    {
    }
}

