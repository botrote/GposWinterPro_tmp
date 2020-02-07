using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : NPC, IMeleeAttack
{
    private const string unitname = "Soldier";
    private const uint soldierNotch = 1;
    private const uint soldierExp = 1;
    private const uint soldierHealth = 50;
    private const uint soldierAttack = 5;
    private const uint soldierDefense = 1;
    private const float soldierMeleeRange = 3.0f;
    private const float soldierSpeed = 1.0f;
    private const Race soldierRace = Race.Soldier;
    private const float soldierMeleeCool = 3.0f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override uint Notch
    {
        get { return soldierNotch; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }
    public override uint NPCMaxHealth
    {
        get { return soldierHealth; }
    }
    public override uint NPCdefense
    {
        get { return soldierDefense; }
    }
    public override float NPCspeed
    {
        get { return soldierSpeed; }
    }
    public override Race race
    {
        get { return soldierRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }

    public override uint Exp
    {
        get { return soldierExp; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if ( Vector2.Distance(Target.position, this.position) <= soldierMeleeRange)
        {
            if (soldierMeleeCool > MeleeCool) return;
            Target.Damage(soldierAttack);
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
        if(MeleeCool <= soldierMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return soldierMeleeRange;
    }
}
