using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : NPC , IMissileAttack
{
    private const string unitname = "Cleric";
    private const int ClericNotch = 1;
    private const int ClericExp = 5;
    private const int ClericHealth = 30;
    private const int ClericAttack = 10;
    private const int ClericDefense = 0;
    private const float ClericMissileRange = 5.0f;
    private const float ClericSpeed = 4.0f;
    private const Race ClericRace = Race.Soldier;
    private const float ClericMissileCool = 2.0f;
    private float MissileCool;
    private const float ClericHealCool = 2.0f;
    private float HealCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return ClericNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return ClericHealth; }
    }
    public override int NPCdefense
    {
        get { return ClericDefense; }
    }
    public override float NPCspeed
    {
        get { return ClericSpeed; }
    }
    public override Race race
    {
        get { return ClericRace; }
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

    public void Shoot(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= ClericMissileRange)
        {
            if (ClericMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Holy", this, this.position, Target.position, (int)ClericAttack, 10f, 1f);
            MissileCool = 0;
        }
    }
    public void Shoot(Vector2 pos)
    {
        if (isStunned) return;
    }

    protected override void Init()
    {
        MissileCool = 0;
        HealCool=0;
        unlock_cost = int.MaxValue;
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
        if (MissileCool <= ClericMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
        if ( HealCool<= ClericHealCool)
        {
            HealCool += Time.deltaTime;
            if (HealCool>=ClericHealCool)
            { 
                AreaHeal();
                HealCool = 0;
            }
        }
    }
    private void AreaHeal()
    {
        Collider2D[] Targets = Physics2D.OverlapCircleAll(this.position, 7.0f);
        for(int i=0; i<Targets.Length; i++)
        {
            if(Targets[i]==null) break;
            else if(Targets[i].tag.Equals("Enemy")) Targets[i].gameObject.GetComponent<Unit>().Heal(20);
             
        }
    }


    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return ClericMissileRange;
    }
}