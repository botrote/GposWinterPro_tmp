using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMagicAttack
{
    void MagicAttack();
    void MagicAttack(Unit target);
    void MagicAttack(Vector2 pos);
    ///Skill skill;
    ///float RateOfAttack;
}
