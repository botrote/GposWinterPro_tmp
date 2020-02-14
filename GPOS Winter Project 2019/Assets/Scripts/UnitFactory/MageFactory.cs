using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFactory : UnitFactory
{
    protected const string product = "Mage";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Mage") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
