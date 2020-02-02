using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 원거리 공격(화살 등)
/// </summary>
public interface IMissileAttack
{
    /// <summary>
    /// 원거리 공격 사정거리 반환
    /// </summary>
    /// <returns>원거리 공격 사정거리</returns>
    float getMissileRange();
    /// <summary>
    /// 다른 유닛을 향해 투사체 발사
    /// </summary>
    /// <param name="Target">대상 유닛</param>
    void Shoot(Unit Target);
    /// <summary>
    /// 특정 위치를 향해 투사체/마법 발사
    /// </summary>
    /// <param name="pos">대상 위치</param>
    void Shoot(Vector2 pos);
}
