using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSFactory : UnitFactory
{
    protected const string product = "SkeletonS";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/SkeletonS") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
