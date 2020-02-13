using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : NPC , IHealer
{
    private const string unitname = "Ghost";
    private const int GhostNotch = 5;
    private const int GhostHealth = 15;
    private const int GhostDefense = 0;
    private const int GhostHeal = 10;
    private const float GhostHealRange = 2.0f;
    private const float GhostSpeed = 3.0f;
    private const Race GhostRace = Race.Undead;
    private const float GhostHealCool = 1.0f;
    private float HealCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return GhostNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return GhostHealth; }
    }
    public override int NPCdefense
    {
        get { return GhostDefense; }
    }
    public override float NPCspeed
    {
        get { return GhostSpeed; }
    }
    public override Race race
    {
        get { return GhostRace; }
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

    public void Heal(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= GhostHealRange)
        {
            if (GhostHealCool > HealCool) return;
            Target.Heal(GhostHeal);
            HealCool = 0;
        }
    }

    protected override void Init()
    {
        HealCool = 0;
        //skill = new Skill();
        unlock_cost = 15;
    }

    void Awake()
    {
        base.Awake();
    }

    //Update is called once per frame
    void Update()
    {
        base.Update();
        if (HealCool <= GhostHealCool)
        {
            HealCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getHealRange()
    {
        return GhostHealRange;
    }
}