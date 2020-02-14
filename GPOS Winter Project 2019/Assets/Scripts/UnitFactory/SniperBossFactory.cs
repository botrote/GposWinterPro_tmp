using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBossFactory : UnitFactory
{
    protected const string product = "SniperBoss";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/SniperBoss") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
