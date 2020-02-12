using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathFactory : ProjectileFactory
{
    private const string product = "Breath";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }
    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Breath") as GameObject;
    }

    public override GameObject MakeProjectile(int damage, float duration, Unit.Team team, params object[] parameter)
    {
        GameObject Instance = GameObject.Instantiate(template);
        Instance.GetComponent<Breath>().Init(team, damage, duration);
        return Instance;
    }
}
