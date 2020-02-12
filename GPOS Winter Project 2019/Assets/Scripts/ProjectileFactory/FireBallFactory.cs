﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallFactory : ProjectileFactory
{
    private const string product = "FireBall";
    protected GameObject template;
    public override string Product
    {
        get { return product; }
    }
    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/FireBall") as GameObject;
    }

    public override GameObject MakeProjectile(int damage, float duration, Unit.Team team, params object[] parameter)
    {
        GameObject Instance = GameObject.Instantiate(template);
        Instance.GetComponent<FireBall>().Init(team, damage, duration, (float)parameter[0]);
        return Instance;
    }
}
