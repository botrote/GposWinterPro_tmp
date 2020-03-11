using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : IBuff
{
    public const float Duration = 1.0f;
    protected float remainTime;
    public Stun()
    {
        remainTime = Duration;
    }
    public Stun(float duration)
    {
        remainTime = duration;
    }
    public float getAttBuff()
    {
        return 1;
    }

    public float getDefBuff()
    {
        return 1;
    }

    public int getHPBuff()
    {
        return 0;
    }

    public string getName()
    {
        return "Stun";
    }

    public float getRemainingTime()
    {
        return remainTime;
    }

    public float getSpdBuff()
    {
        return 1;
    }

    public bool isStun()
    {
        return true;
    }

    public bool Update(float time)
    {
        remainTime -= time;
        if (remainTime <= 0) return false;
        else return true;
    }
}