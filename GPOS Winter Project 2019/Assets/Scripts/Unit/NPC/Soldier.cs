using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : NPC , IMeleeAttack
{
    private const ushort soldierNotch = 1;
    private const ushort soldierHealth = 50;
    private const ushort soldierAttack = 5;
    private const ushort soldierDefense = 1;
    private const float soldierRange = 4.0f;
    private const float soldierSpeed = 1.0f;
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

    public void MeleeAttack(Unit Target)
    {
        if( Vector2.Distance(Target.position, this.position) <= soldierRange)
        {
            if (soldierMeleeCool > MeleeCool) return;
            Target.Damage(soldierAttack);
            MeleeCool = 0;
        }
        else if (Vector2.Distance(Target.position, this.Dest) > soldierRange + 1)
        {
            this.Dest = Target.position;
        }
    }

    protected virtual void Init()
    {
        //ai = new AI();
        //skill = new Skill();
        RateOfSpecialAttack = 0;
        curHealth = MaxHealth;
        defense = soldierDefense;
        speed = soldierSpeed;
        race = Race.Undead;
    }

    void Awake()
    {
        base.Awake();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(MeleeCool <= soldierMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
        MeleeAttack(GameObject.FindGameObjectWithTag(Team.Friendly.ToString()).GetComponent<Unit>());
    }

    private void OnDestroy()
    {

    }
}
