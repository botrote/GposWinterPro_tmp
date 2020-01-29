using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : NPC , IMeleeAttack
{
    private const ushort zombieNotch = 1;
    private const ushort zombieHealth = 50;
    private const ushort zombieAttack = 5;
    private const ushort zombieDefense = 1;
    private const float zombieRange = 0;
    private const float zombieSpeed = 1.0f;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }

    public override ushort Notch
    {
        get { return zombieNotch; }
    }

    public override ushort MaxHealth
    {
        get { return zombieHealth; }
    }

    public void MeleeAttack(Unit Target)
    {

    }

    protected virtual void Init()
    {
        //ai = new AI();
        //skill = new Skill();
        RateOfSpecialAttack = 0;
        curHealth = MaxHealth;
        defense = zombieDefense;
        speed = zombieSpeed;
        race = Race.Undead;
    }

    void Awake()
    {
        base.Awake();
        Init();
        Dest = new Vector2(10, 0);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    base.Update();
    //}

    private void OnDestroy()
    {

    }
}
