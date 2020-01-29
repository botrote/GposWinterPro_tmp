using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRangedAttack
{
    void Shoot(Unit Target); //다른 유닛을 향해 투사체/마법 발사
    void Shoot(Vector2 pos); //특정 위치를 향해 투사체/마법 발사
}
