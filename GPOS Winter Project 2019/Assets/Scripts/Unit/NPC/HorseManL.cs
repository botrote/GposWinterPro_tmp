using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManL : NPC, IMeleeAttack
{
    private const string unitname = "HorseManL";
    private const int HorseManLNotch = 1;
    private const int HorseManLExp = 1;
    private const int HorseManLHealth = 100;
    private const int HorseManLAttack = 10;
    private const int HorseManLChargeAttack = 50;
    private const int HorseManLDefense = 10;
    private const float HorseManLMeleeRange = 1.0f;
    private const float HorseManLChargeRange = 2.0f;
    private const float HorseManLSpeed = 5f;
    private const float HorseManLChargeSpeed = 7f;
    private const Race HorseManLRace = Race.Soldier;
    private const float HorseManLMeleeCool = 2f;
    private float MeleeCool;
    public bool charge;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return HorseManLNotch; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }
    public override int NPCMaxHealth
    {
        get { return HorseManLHealth; }
    }
    public override int NPCdefense
    {
        get { return HorseManLDefense; }
    }
    public override float NPCspeed
    {
        get { return charge ? HorseManLChargeSpeed : HorseManLSpeed; }
    }
    public override Race race
    {
        get { return HorseManLRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }

    public override int Exp
    {
        get { return HorseManLExp; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if ( Vector2.Distance(Target.position, this.position) <= (charge? HorseManLChargeRange : HorseManLMeleeRange))
        {
            if (HorseManLMeleeCool > MeleeCool) return;
            Target.Damage(charge? HorseManLChargeAttack : HorseManLAttack);
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
        if(MeleeCool <= HorseManLMeleeCool)
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
        return charge? HorseManLChargeRange : HorseManLMeleeRange;
    }
}
