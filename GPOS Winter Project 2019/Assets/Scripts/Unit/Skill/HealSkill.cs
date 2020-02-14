using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : ISkill
{
    private const int heal = 20;
    public const int Hcost = 0;
    public const int HunlockCost = 0;

    public override void getCost(out int decknotch, out int unlockCost)
    {
        decknotch = Hcost;
        unlockCost = HunlockCost;
    }

    public override void UseSkill()
    {
        GameObject.Find("Player").GetComponent<Unit>().Heal(20);
    }

    public override void UseSkill(Unit user, Unit Target)
    {
        Target.Heal(20);
    }

    public override void UseSkill(Unit user, Vector2 pos)
    {
    }
}

