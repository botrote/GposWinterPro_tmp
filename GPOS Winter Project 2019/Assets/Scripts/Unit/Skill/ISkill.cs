using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void UseSkill();
    void UseSkill(Unit Target);
    void UseSkill(Vector2 pos);
}

