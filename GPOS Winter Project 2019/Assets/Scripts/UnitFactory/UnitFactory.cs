using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFactory : MonoBehaviour
{
    public abstract string Product { get; }
    public abstract GameObject MakeUnit(params object[] parameter);
}
