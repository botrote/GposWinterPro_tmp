using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManBFactory : UnitFactory
{
    protected const string product = "HorseManB";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/HorseManB") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
