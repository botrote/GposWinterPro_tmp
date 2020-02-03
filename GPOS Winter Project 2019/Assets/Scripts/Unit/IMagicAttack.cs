using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 마법 사용 인터페이스, 공격 이외에도 힐 등 포함
/// </summary>
public interface IMagicAttack
{
    /// <summary>
    /// 마법 사정거리 반환
    /// </summary>
    /// <returns>마법 공격 사정거리</returns>
    float getMagicRange();
    /// <summary>
    /// 마법 사용
    /// </summary>
    void MagicAttack();
    /// <summary>
    /// 다른 유닛에게 마법 사용함
    /// </summary>
    /// <param name="target">대상 유닛</param>
    void MagicAttack(Unit target);
    /// <summary>
    /// 특정 좌표에 마법 사용함
    /// </summary>
    /// <param name="pos">대상 좌표</param>
    void MagicAttack(Vector2 pos);
}
