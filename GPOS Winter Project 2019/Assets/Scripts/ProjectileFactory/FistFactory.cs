using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistFactory : ProjectileFactory
{
    private const string product = "Fist";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }
    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Fist") as GameObject;
    }

    public override GameObject MakeProjectile(int damage, float duration, Unit.Team team, params object[] parameter)
    {
        GameObject Instance = GameObject.Instantiate(template);
        Instance.GetComponent<Fist>().Init(team, damage, duration);
        return Instance;
    }
}
