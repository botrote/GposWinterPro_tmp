using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManBoss : NPC, IMeleeAttack
{
    private const string unitname = "HorseManBoss";
    private const int HorseManBossNotch = 1;
    private const int HorseManBossExp = 20;
    private const int HorseManBossHealth = 400;
    private const int HorseManBossAttack = 28;
    private const int HorseManBossChargeAttack = 70;
    private const float DamageRadius = 0.8f;
    private const int HorseManBossDefense = 20;
    private const float HorseManBossMeleeRange = 1.0f;
    private const float HorseManBossChargeRange = 2.0f;
    private const float HorseManBossSpeed = 7f;
    private const float HorseManBossChargeSpeed = 7f;
    private const Race HorseManBossRace = Race.Elite;
    private const float HorseManBossMeleeCool = 1.5f;
    private float MeleeCool;
    public bool charge;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return HorseManBossNotch; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }
    public override int NPCMaxHealth
    {
        get { return HorseManBossHealth; }
    }
    public override int NPCdefense
    {
        get { return HorseManBossDefense; }
    }
    public override float NPCspeed
    {
        get { return charge ? HorseManBossChargeSpeed : HorseManBossSpeed; }
    }
    public override Race race
    {
        get { return HorseManBossRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }

    public override int Exp
    {
        get { return HorseManBossExp; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if ( Vector2.Distance(Target.position, this.position) <= (charge? HorseManBossChargeRange : HorseManBossMeleeRange))
        {
            if (HorseManBossMeleeCool > MeleeCool) return;
            if(charge)
            {
                Collider2D[] Targets = Physics2D.OverlapCircleAll(Target.position, DamageRadius);
                for(int i=0; i<Targets.Length; i++)
                {
                    if(Targets[i].gameObject.GetComponent<Unit>()==null) continue;
                    else if ( Targets[i].gameObject.GetComponent<Unit>().tag.Equals("Friendly"))
                    {
                        Targets[i].gameObject.GetComponent<Unit>().Damage(HorseManBossChargeAttack);
                    }
                }
            } 
            else Target.Damage(HorseManBossAttack);
            MeleeCool = 0;
        }
    }

    protected override void Init()
    {
        MeleeCool = 0;
        //skill = new Skill();
        unlock_cost = int.MaxValue;
    }

    void Awake()
    {
        base.Awake();
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(MeleeCool <= HorseManBossMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
        if (isStunned) charge=false;
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return charge? HorseManBossChargeRange : HorseManBossMeleeRange;
    }
}
