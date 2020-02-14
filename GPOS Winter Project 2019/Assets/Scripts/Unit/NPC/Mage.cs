using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : NPC , IMissileAttack
{
    private const string unitname = "Mage";
    private const int MageNotch = 2;
    private const int ArcherExp = 5;
    private const int MageHealth = 30;
    private const int MageAttack = 15;
    private const int MageDefense = 0;
    private const float MageMissileRange = 5f;
    private const float MageDamageRadius = 1f;
    private const float MageSpeed = 4.0f;
    private const Race MageRace = Race.None;
    private const float MageMissileCool = 1f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return MageNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return MageHealth; }
    }
    public override int NPCdefense
    {
        get { return MageDefense; }
    }
    public override float NPCspeed
    {
        get { return MageSpeed; }
    }
    public override Race race
    {
        get { return MageRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= MageMissileRange)
        {
            if (MageMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Flame", this, this.position, Target.position, (int)(MageAttack), 10f, 1f, MageDamageRadius);
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
        //skill = new Skill();
        unlock_cost = int.MaxValue;
    }

    void Awake()
    {
        base.Awake();
    }

    //Update is called once per frame
    void Update()
    {
        base.Update();
        if (MissileCool <= MageMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return MageMissileRange;
    }
}