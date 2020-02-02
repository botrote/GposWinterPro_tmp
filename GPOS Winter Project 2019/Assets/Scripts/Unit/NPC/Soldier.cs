using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : NPC, IMeleeAttack
{
    private const string unitname = "Soldier";
    private const ushort soldierNotch = 1;
    private const ushort soldierExp = 1;
    private const ushort soldierHealth = 50;
    private const ushort soldierAttack = 5;
    private const ushort soldierDefense = 1;
    private const float soldierMeleeRange = 1.0f;
    private const float soldierSpeed = 1.0f;
    private const Race soldierRace = Race.Soldier;
    private const float soldierMeleeCool = 3.0f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override ushort Notch
    {
        get { return soldierNotch; }
    }
    public override ushort MaxHealth
    {
        get { return soldierHealth; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }
    public override ushort defense
    {
        get { return soldierDefense; }
    }
    public override float speed
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

    public override ushort Exp
    {
        get { return soldierExp; }
    }

    public void MeleeAttack(Unit Target)
    {
        if( Vector2.Distance(Target.position, this.position) <= soldierMeleeRange)
        {
            if (soldierMeleeCool > MeleeCool) return;
            Target.Damage(soldierAttack);
            MeleeCool = 0;
        }
    }

    protected override void Init()
    {
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
