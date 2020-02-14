using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terror : IBuff
{
    public const float Duration = 0.2f;
    protected float remainTime;
    public Terror()
    {
        remainTime = Duration;
    }
    public float getAttBuff()
    {
        return 1;
    }

    public float getDefBuff()
    {
        return 0.5f;
    }

    public int getHPBuff()
    {
        return 0;
    }

    public string getName()
    {
        return "Terror";
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
        return false;
    }

    public bool Update(float time)
    {
        remainTime -= time;
        if (remainTime <= 0) return false;
        else return true;
    }
}