using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFactory : UnitFactory
{
    protected const string product = "Zombie";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        template = Resources.Load("Prefabs/Zombie") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
