using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : NPC , IMissileAttack
{
    private const string unitname = "Dragon";
    private const uint DragonNotch = 4;
    private const uint DragonHealth = 30;
    private const uint DragonAttack = 15;
    private const uint DragonDefense = 0;
    private const float DragonMissileRange = 5.0f;
    private const float DragonDamageRadius = 1.0f;
    private const float DragonSpeed = 5.0f;
    private const Race DragonRace = Race.Undead;
    private const float DragonMissileCool = 1.2f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint Notch
    {
        get { return DragonNotch; }
    }
    public override uint NPCMaxHealth
    {
        get { return DragonHealth; }
    }
    public override uint NPCdefense
    {
        get { return DragonDefense; }
    }
    public override float NPCspeed
    {
        get { return DragonSpeed; }
    }
    public override Race race
    {
        get { return DragonRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= DragonMissileRange)
        {
            if (DragonMissileCool > MissileCool) return;
            else
            {
                Collider2D[] Targets = Physics2D.OverlapCircleAll(Target.position, DragonDamageRadius);
                for(int i=0; i<Targets.Length; i++)
                {
                    if(Targets[i].gameObject.GetComponent<Unit>().TeamTag.Equals("Enemy")) Targets[i].gameObject.GetComponent<Unit>().Damage((uint)(DragonAttack * friendlyAttackFactor));
                }
                MissileCool = 0;
            }
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
        if (MissileCool <= DragonMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return DragonMissileRange;
    }
}