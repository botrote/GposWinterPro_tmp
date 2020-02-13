using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : NPC, IMeleeAttack
{
    private const string unitname = "Soldier";
    private const int soldierNotch = 1;
    private const int soldierExp = 1;
    private const int soldierHealth = 30;
    private const int soldierAttack = 10;
    private const int soldierDefense = 0;
    private const float soldierMeleeRange = 1.0f;
    private const float soldierSpeed = 4.0f;
    private const Race soldierRace = Race.Soldier;
    private const float soldierMeleeCool = 1.2f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return soldierNotch; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }
    public override int NPCMaxHealth
    {
        get { return soldierHealth; }
    }
    public override int NPCdefense
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

    public override int Exp
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
