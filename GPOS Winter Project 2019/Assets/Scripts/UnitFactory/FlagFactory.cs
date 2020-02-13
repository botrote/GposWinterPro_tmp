using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagFactory : UnitFactory
{
    protected const string product = "Flag";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Flag") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
