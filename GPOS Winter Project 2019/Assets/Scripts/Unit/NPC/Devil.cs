using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : NPC , IMissileAttack
{
    private const string unitname = "Devil";
    private const int DevilNotch = 1;
    private const int DevilHealth = 80;
    private const int DevilAttack = 0;
    private const int DevilDefense = 0;
    private const float DevilMissileRange = 1.5f;
    private const float DevilSpeed = 4.0f;
    private const Race DevilRace = Race.None;
    private const float DevilMissileCool = 1.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return DevilNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return DevilHealth; }
    }
    public override int NPCdefense
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

    public override int Exp
    {
        get { return 0; }
    }

    public void Shoot(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= DevilMissileRange)
        {
            if (DevilMissileCool > MissileCool) return;
            Target.Damage((int)(DevilAttack * friendlyAttackFactor));
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
        unlock_cost = 200;
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