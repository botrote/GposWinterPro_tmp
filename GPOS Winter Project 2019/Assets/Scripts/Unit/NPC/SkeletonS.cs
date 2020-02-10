using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonS : NPC , IMeleeAttack
{
    private const string unitname = "SkeletonS";
    private const uint SkeletonSNotch = 1;
    private const uint SkeletonSHealth = 30;
    private const uint SkeletonSAttack = 10;
    private const uint SkeletonSDefense = 1;
    private const float SkeletonSMeleeRange = 1.0f;
    private const float SkeletonSSpeed = 5.0f;
    private const Race SkeletonSRace = Race.Undead;
    private const float SkeletonSMeleeCool = 1.0f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint Notch
    {
        get { return SkeletonSNotch; }
    }
    public override uint NPCMaxHealth
    {
        get { return SkeletonSHealth; }
    }
    public override uint NPCdefense
    {
        get { return SkeletonSDefense; }
    }
    public override float NPCspeed
    {
        get { return SkeletonSSpeed; }
    }
    public override Race race
    {
        get { return SkeletonSRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }

    public override uint Exp
    {
        get { return 0; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= SkeletonSMeleeRange)
        {
            if (SkeletonSMeleeCool > MeleeCool) return;
            Target.Damage((uint)(SkeletonSAttack * friendlyAttackFactor));
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

    //Update is called once per frame
    void Update()
    {
        base.Update();
        if (MeleeCool <= SkeletonSMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return SkeletonSMeleeRange;
    }
}