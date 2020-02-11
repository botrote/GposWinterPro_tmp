using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFactory : UnitFactory
{
    protected const string product = "Dragon";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Dragon") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
