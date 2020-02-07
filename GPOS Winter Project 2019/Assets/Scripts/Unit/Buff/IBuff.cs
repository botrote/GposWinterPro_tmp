using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버프, 디버프
/// </summary>
public interface IBuff
{
    float getDefBuff();
    float getSpdBuff();
    float getAttBuff();
    int getHPBuff();
    bool isStun();
}
