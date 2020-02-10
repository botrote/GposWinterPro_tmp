using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonB : NPC , IMissileAttack
{
    private const string unitname = "SkeletonB";
    private const uint SkeletonBNotch = 1;
    private const uint SkeletonBHealth = 25;
    private const uint SkeletonBAttack = 10;
    private const uint SkeletonBDefense = 1;
    private const float SkeletonBMissileRange = 5.0f;
    private const float SkeletonBSpeed = 3.0f;
    private const Race SkeletonBRace = Race.Undead;
    private const float SkeletonBMissileCool = 1.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint Notch
    {
        get { return SkeletonBNotch; }
    }
    public override uint NPCMaxHealth
    {
        get { return SkeletonBHealth; }
    }
    public override uint NPCdefense
    {
        get { return SkeletonBDefense; }
    }
    public override float NPCspeed
    {
        get { return SkeletonBSpeed; }
    }
    public override Race race
    {
        get { return SkeletonBRace; }
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

    public void Shoot(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= SkeletonBMissileRange)
        {
            if (SkeletonBMissileCool > MissileCool) return;
            Target.Damage((uint)(SkeletonBAttack * friendlyAttackFactor));
            MissileCool = 0;
        }
    }
    public void Shoot(Vector2 pos)
    {
        if (isStunned) return;
    }

    protected override void Init()
    {
        MissileCool = 0;
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
        if (MissileCool <= SkeletonBMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return SkeletonBMissileRange;
    }
}