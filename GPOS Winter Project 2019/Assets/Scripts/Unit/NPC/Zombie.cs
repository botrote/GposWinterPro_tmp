using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : NPC , IMeleeAttack
{
    private const string unitname = "Zombie";
    private const ushort zombieNotch = 1;
    private const ushort zombieHealth = 50;
    private const ushort zombieAttack = 5;
    private const ushort zombieDefense = 1;
    private const float zombieMeleeRange = 1.0f;
    private const float zombieSpeed = 1.0f;
    private const Race zombieRace = Race.Undead;

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
    public override ushort defense
    {
        get { return zombieDefense; }
    }
    public override float speed
    {
        get { return zombieSpeed; }
    }
    public override Race race
    {
        get { return zombieRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }

    public override ushort Exp
    {
        get { return 0; }
    }

    public void MeleeAttack(Unit Target)
    {

    }

    protected override void Init()
    {
        //ai = new FriendlyMeleeAI<Zombie>(this);
        //skill = new Skill();
    }

    void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    base.Update();
    //}

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return zombieMeleeRange;
    }
}
