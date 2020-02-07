using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : NPC , IMeleeAttack
{
    private const string unitname = "Orc";
    private const uint OrcNotch = 2;
    private const uint OrcHealth = 60;
    private const uint OrcAttack = 7;
    private const uint OrcDefense = 1;
    private const float OrcMeleeRange = 1.0f;
    private const float OrcSpeed = 1.2f;
    private const Race OrcRace = Race.None;
    private const float OrcMeleeCool = 1.2f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint Notch
    {
        get { return OrcNotch; }
    }
    public override uint NPCMaxHealth
    {
        get { return OrcHealth; }
    }
    public override uint NPCdefense
    {
        get { return OrcDefense; }
    }
    public override float NPCspeed
    {
        get { return OrcSpeed; }
    }
    public override Race race
    {
        get { return OrcRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= OrcMeleeRange)
        {
            if (OrcMeleeCool > MeleeCool) return;
            Target.Damage((uint)(OrcAttack * friendlyAttackFactor));
            MeleeCool = 0;
            if(Random.Range(0f,1.0f)<=0.2f) Target.Buffs.Add(new Stun());
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
        if (MeleeCool <= OrcMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return OrcMeleeRange;
    }
}