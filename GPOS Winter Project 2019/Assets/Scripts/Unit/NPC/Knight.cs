using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : NPC, IMeleeAttack
{
    private const string unitname = "Knight";
    private const int KnightNotch = 1;
    private const int KnightExp = 1;
    private const int KnightHealth = 60;
    private const int KnightAttack = 25;
    private const int KnightDefense = 50;
    private const float KnightMeleeRange = 1.5f;
    private const float KnightSpeed = 5.0f;
    private const Race KnightRace = Race.Soldier;
    private const float KnightMeleeCool = 2f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return KnightNotch; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }
    public override int NPCMaxHealth
    {
        get { return KnightHealth; }
    }
    public override int NPCdefense
    {
        get { return KnightDefense; }
    }
    public override float NPCspeed
    {
        get { return KnightSpeed; }
    }
    public override Race race
    {
        get { return KnightRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }

    public override int Exp
    {
        get { return KnightExp; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if ( Vector2.Distance(Target.position, this.position) <= KnightMeleeRange)
        {
            if (KnightMeleeCool > MeleeCool) return;
            Target.Damage(KnightAttack);
            MeleeCool = 0;
        }
    }

    protected override void Init()
    {
        MeleeCool = 0;
        //skill = new Skill();
    }

    void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(MeleeCool <= KnightMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return KnightMeleeRange;
    }
}
