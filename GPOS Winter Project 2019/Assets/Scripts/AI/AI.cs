using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI : MonoBehaviour
{
    protected Unit body;

    public AI(Unit paramUnit)
    {
        body = paramUnit;
    }
}
