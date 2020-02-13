using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichKing : NPC , IMissileAttack
{
    private const string unitname = "LichKing";
    private const int LichKingNotch = 4;
    private const int LichKingHealth = 30;
    private const int LichKingAttack = 15;
    private const int LichKingDefense = 0;
    private const float LichKingMissileRange = 5.0f;
    private const float LichKingDamageRadius = 1.0f;
    private const float LichKingSpeed = 5.0f;
    private const Race LichKingRace = Race.Undead;
    private const float LichKingMissileCool = 1.2f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return LichKingNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return LichKingHealth; }
    }
    public override int NPCdefense
    {
        get { return LichKingDefense; }
    }
    public override float NPCspeed
    {
        get { return LichKingSpeed; }
    }
    public override Race race
    {
        get { return LichKingRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= LichKingMissileRange)
        {
            if (LichKingMissileCool > MissileCool) return;
            else
            {
                Collider2D[] Targets = Physics2D.OverlapCircleAll(Target.position, LichKingDamageRadius);
                for(int i=0; i<Targets.Length; i++)
                {
                    if(Targets[i].gameObject.GetComponent<Unit>().TeamTag.Equals("Enemy")) Targets[i].gameObject.GetComponent<Unit>().Damage((int)(LichKingAttack * friendlyAttackFactor));
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
        if (MissileCool <= LichKingMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return LichKingMissileRange;
    }
}