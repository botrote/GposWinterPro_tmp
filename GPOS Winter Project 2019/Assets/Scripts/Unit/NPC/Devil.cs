using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : NPC , IMissileAttack
{
    private const string unitname = "Devil";
    private const uint DevilNotch = 1;
    private const uint DevilHealth = 80;
    private const uint DevilAttack = 0;
    private const uint DevilDefense = 0;
    private const float DevilMissileRange = 1.5f;
    private const float DevilSpeed = 4.0f;
    private const Race DevilRace = Race.None;
    private const float DevilMissileCool = 1.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint Notch
    {
        get { return DevilNotch; }
    }
    public override uint NPCMaxHealth
    {
        get { return DevilHealth; }
    }
    public override uint NPCdefense
    {
        get { return DevilDefense; }
    }
    public override float NPCspeed
    {
        get { return DevilSpeed; }
    }
    public override Race race
    {
        get { return DevilRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= DevilMissileRange)
        {
            if (DevilMissileCool > MissileCool) return;
            Target.Damage((uint)(DevilAttack * friendlyAttackFactor));
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
        //DevilAttack=GameObject.Find("Player").attack;
        base.Awake();
    }

    //Update is called once per frame
    void Update()
    {
        base.Update();
        if (MissileCool <= DevilMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return DevilMissileRange;
    }
}