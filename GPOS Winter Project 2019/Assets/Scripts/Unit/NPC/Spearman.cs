using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearman : NPC, IMeleeAttack
{
    private const string unitname = "Spearman";
    private const uint SpearmanNotch = 1;
    private const uint SpearmanExp = 1;
    private const uint SpearmanHealth = 20;
    private const uint SpearmanAttack = 20;
    private const uint SpearmanDefense = 0;
    private const float SpearmanMeleeRange = 2.0f;
    private const float SpearmanSpeed = 3.5f;
    private const Race SpearmanRace = Race.Soldier;
    private const float SpearmanMeleeCool = 2.5f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override uint Notch
    {
        get { return SpearmanNotch; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }
    public override uint NPCMaxHealth
    {
        get { return SpearmanHealth; }
    }
    public override uint NPCdefense
    {
        get { return SpearmanDefense; }
    }
    public override float NPCspeed
    {
        get { return SpearmanSpeed; }
    }
    public override Race race
    {
        get { return SpearmanRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }

    public override uint Exp
    {
        get { return SpearmanExp; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if ( Vector2.Distance(Target.position, this.position) <= SpearmanMeleeRange)
        {
            if (SpearmanMeleeCool > MeleeCool) return;
            Target.Damage(SpearmanAttack);
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
        if(MeleeCool <= SpearmanMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return SpearmanMeleeRange;
    }
}
