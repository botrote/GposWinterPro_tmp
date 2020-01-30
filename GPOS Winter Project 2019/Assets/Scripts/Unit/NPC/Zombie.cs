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
    private const float zombieRange = 0;
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
    protected override ushort defense
    {
        get { return zombieDefense; }
    }
    protected override float speed
    {
        get { return zombieSpeed; }
    }
    protected override Race race
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

    public void MeleeAttack(Unit Target)
    {

    }

    protected override void Init()
    {
        //ai = new AI();
        //skill = new Skill();
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
