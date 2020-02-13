using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void UseSkill();
    void UseSkill(Unit user, Unit Target);
    void UseSkill(Unit user, Vector2 pos);
}

