using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 힐
/// </summary>
public interface IHealer
{
    /// <summary>
    /// 힐 사정거리 반환
    /// </summary>
    /// <returns>힐 사정거리</returns>
    float getHealRange();
    /// <summary>
    /// 다른 유닛을 힐
    /// </summary>
    /// <param name="Target">대상 유닛</param>
    void Heal(Unit Target);
}
