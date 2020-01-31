using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagicAttack
{
    float getMagicRange();
    void MagicAttack();
    void MagicAttack(Unit target);
    void MagicAttack(Vector2 pos);
    ///Skill skill;
    ///float RateOfAttack;
}
