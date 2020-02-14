using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : NPC , IMissileAttack
{
    private const string unitname = "Sniper";
    private const int SniperNotch = 1;
    private const int SniperExp = 1;
    private const int SniperHealth = 30;
    private const int SniperAttack = 25;
    private const int SniperDefense = 0;
    private const float SniperMissileRange = 10.0f;
    private const float SniperSpeed = 2.0f;
    private const Race SniperRace = Race.Soldier;
    private const float SniperMissileCool = 2.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return SniperNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return SniperHealth; }
    }
    public override int NPCdefense
    {
        get { return SniperDefense; }
    }
    public override float NPCspeed
    {
        get { return SniperSpeed; }
    }
    public override Race race
    {
        get { return SniperRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= SniperMissileRange)
        {
            if (SniperMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Bolt", this, this.position, Target.position, (int)SniperAttack, 15f, 1f);
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
        if (MissileCool <= SniperMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return SniperMissileRange;
    }
}