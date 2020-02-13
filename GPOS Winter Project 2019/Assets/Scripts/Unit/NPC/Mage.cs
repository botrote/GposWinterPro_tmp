using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : NPC , IMissileAttack
{
    private const string unitname = "Mage";
    private const int MageNotch = 2;
    private const int MageHealth = 20;
    private const int MageAttack = 20;
    private const int MageDefense = 0;
    private const float MageMissileRange = 3.5f;
    private const float MageDamageRadius = 1.5f;
    private const float MageSpeed = 3.0f;
    private const Race MageRace = Race.None;
    private const float MageMissileCool = 2.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
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
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Explosive", this, this.position, Target.position, (int)(MageAttack*friendlyAttackFactor), 10f, 1f, MageDamageRadius);
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