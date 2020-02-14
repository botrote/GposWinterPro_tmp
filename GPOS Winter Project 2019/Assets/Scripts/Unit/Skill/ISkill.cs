using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISkill
{
    public abstract void UseSkill();
    public abstract void UseSkill(Unit user, Unit Target);
    public abstract void UseSkill(Unit user, Vector2 pos);
    public abstract void getCost(out int decknotch, out int unlockCost);
    public static void getCost<T>(out int decknotch, out int unlockCost) where T : ISkill, new()
    {
        T instance = new T();
        instance.getCost(out decknotch, out unlockCost);
    }
}

