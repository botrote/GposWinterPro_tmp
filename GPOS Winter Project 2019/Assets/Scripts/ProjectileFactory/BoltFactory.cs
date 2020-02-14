using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltFactory : ProjectileFactory
{
    private const string product = "Bolt";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }
    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Bolt") as GameObject;
    }

    public override GameObject MakeProjectile(int damage, float duration, Unit.Team team, params object[] parameter)
    {
        GameObject Instance = GameObject.Instantiate(template);
        Instance.GetComponent<Bolt>().Init(team, damage, duration);
        return Instance;
    }
}
