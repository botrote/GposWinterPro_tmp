using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyFactory : ProjectileFactory
{
    private const string product = "Holy";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }
    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Holy") as GameObject;
    }

    public override GameObject MakeProjectile(int damage, float duration, Unit.Team team, params object[] parameter)
    {
        GameObject Instance = GameObject.Instantiate(template);
        Instance.GetComponent<Arrow>().Init(team, damage, duration);
        return Instance;
    }
}
