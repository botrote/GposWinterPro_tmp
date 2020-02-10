using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버프, 디버프
/// </summary>
public interface IBuff
{
    string getName();
    float getDefBuff();
    float getSpdBuff();
    float getAttBuff();
    int getHPBuff();
    bool isStun();
    float getRemainingTime();
    /// <summary>
    /// 남은 시간 체크하고 시간이 다 되었으면 false 반환, 아직 안 되었으면 true 반환
    /// </summary>
    /// <param name="time">저번 체크로부터 지난 시간</param>
    /// <returns></returns>
    bool Update(float time);
}
