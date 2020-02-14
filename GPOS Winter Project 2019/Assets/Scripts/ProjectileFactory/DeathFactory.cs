using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFactory : ProjectileFactory
{
    private const string product = "Death";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }
    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Death") as GameObject;
    }

    public override GameObject MakeProjectile(int damage, float duration, Unit.Team team, params object[] parameter)
    {
        GameObject Instance = GameObject.Instantiate(template);
        Instance.GetComponent<Death>().Init(team, damage, duration);
        return Instance;
    }
}
