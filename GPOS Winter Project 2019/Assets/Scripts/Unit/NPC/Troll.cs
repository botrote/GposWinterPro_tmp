using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : NPC , IMeleeAttack
{
    private const string unitname = "Troll";
    private const uint TrollNotch = 2;
    private const uint TrollHealth = 20;
    private const uint TrollAttack = 10;
    private const uint TrollDefense = 1;
    private const float TrollMeleeRange = 1.0f;
    private const float TrollSpeed = 5.0f;
    private const Race TrollRace = Race.None;
    private const float TrollMeleeCool = 0.7f;
    private float MeleeCool;
    private const float TrollHealCool = 1.0f;
    private float HealCool = 0.0f;
    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint Notch
    {
        get { return TrollNotch; }
    }
    public override uint NPCMaxHealth
    {
        get { return TrollHealth; }
    }
    public override uint NPCdefense
    {
        get { return TrollDefense; }
    }
    public override float NPCspeed
    {
        get { return TrollSpeed; }
    }
    public override Race race
    {
        get { return TrollRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= TrollMeleeRange)
        {
            if (TrollMeleeCool > MeleeCool) return;
            Target.Damage((uint)(TrollAttack * friendlyAttackFactor));
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
        if (MeleeCool <= TrollMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }

        if (HealCool <= TrollHealCool)
        {
            HealCool += Time.deltaTime;
        }
        else
        {
            Heal(MaxHealth/4);
            HealCool=0;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return TrollMeleeRange;
    }
}