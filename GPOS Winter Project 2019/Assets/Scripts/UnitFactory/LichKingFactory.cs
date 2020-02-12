using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichKingFactory : UnitFactory
{
    protected const string product = "LichKing";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/LichKing") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        return GameObject.Instantiate(template);
    }
}
