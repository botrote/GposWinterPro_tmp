using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFactory : UnitFactory
{
    protected const string product = "Ghost";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Ghost") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
