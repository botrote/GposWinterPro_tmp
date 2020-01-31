using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeAttack
{
    float getMeleeRange();
    void MeleeAttack(Unit Target); //다른 유닛을 근접 공격함
}
