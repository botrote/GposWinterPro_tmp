using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericFactory : UnitFactory
{
    protected const string product = "Cleric";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Cleric") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
