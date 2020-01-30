using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : UnitFactory
{
    protected const string product = "Soldier";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        template = Resources.Load("Prefabs/Soldier") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
