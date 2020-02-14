using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : NPC , IMeleeAttack
{
    private const string unitname = "Orc";
    private const int OrcNotch = 2;
    private const int OrcHealth = 60;
    private const int OrcAttack = 7;
    private const int OrcDefense = 10;
    private const float OrcMeleeRange = 1.0f;
    private const float OrcSpeed = 1.2f;
    private const Race OrcRace = Race.None;
    private const float OrcMeleeCool = 1.2f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return OrcNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return OrcHealth; }
    }
    public override int NPCdefense
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

    public override int Exp
    {
        get { return 0; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= OrcMeleeRange)
        {
            if (OrcMeleeCool > MeleeCool) return;
            Target.Damage((int)(OrcAttack * friendlyAttackFactor));
            MeleeCool = 0;
            if(Random.Range(0f,1.0f)<=0.2f) Target.Addbuff(new Stun());
        }
    }

    protected override void Init()
    {
        MeleeCool = 0;
        //skill = new Skill();
        unlock_cost = 30;
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