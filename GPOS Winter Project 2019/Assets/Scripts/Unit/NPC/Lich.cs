using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : NPC , IMissileAttack
{
    private const string unitname = "Lich";
    private const uint LichNotch = 4;
    private const uint LichHealth = 30;
    private const uint LichAttack = 15;
    private const uint LichDefense = 0;
    private const float LichMissileRange = 5.0f;
    private const float LichDamageRadius = 1.0f;
    private const float LichSpeed = 5.0f;
    private const Race LichRace = Race.Undead;
    private const float LichMissileCool = 1.2f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint Notch
    {
        get { return LichNotch; }
    }
    public override uint NPCMaxHealth
    {
        get { return LichHealth; }
    }
    public override uint NPCdefense
    {
        get { return LichDefense; }
    }
    public override float NPCspeed
    {
        get { return LichSpeed; }
    }
    public override Race race
    {
        get { return LichRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }

    public override uint Exp
    {
        get { return 0; }
    }

    public void Shoot(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= LichMissileRange)
        {
            if (LichMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("DeathBall", this, Target.position, Target.position, (int)(LichAttack*friendlyAttackFactor), 0f, 1f, LichDamageRadius);
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
        if (MissileCool <= LichMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return LichMissileRange;
    }
}