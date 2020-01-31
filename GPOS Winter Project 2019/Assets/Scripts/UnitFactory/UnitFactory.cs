using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFactory : MonoBehaviour
{
    protected void Awake()
    {
        FactoryManager.factoryDict.Add(this.Product, this);
    }
    public abstract string Product { get; }
    public abstract GameObject MakeUnit(params object[] parameter);
}
