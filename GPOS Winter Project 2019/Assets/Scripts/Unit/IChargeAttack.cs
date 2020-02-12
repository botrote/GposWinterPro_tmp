using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 차지 공격 인터페이스
/// </summary>
public interface IChargeAttack
{
    /// <summary>
    /// 근접전 사거리 반환
    /// </summary>
    /// <returns>근접전 사거리</returns>
    float getMeleeRange();
    /// <summary>
    /// 다른 유닛을 근접 공격함
    /// </summary>
    /// <param name="Target">목표 유닛</param>
    void MeleeAttack(Unit Target);
    bool GetCharging();
    void SetCharging(bool charge);
}
