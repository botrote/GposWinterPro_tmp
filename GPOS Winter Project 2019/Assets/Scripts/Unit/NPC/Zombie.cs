using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : NPC , IMeleeAttack
{
    private const string unitname = "Zombie";
    private const int zombieNotch = 0;
    private const int zombieHealth = 7;
    private const int zombieAttack = 10;
    private const int zombieDefense = 0;
    private const float zombieMeleeRange = 1.0f;
    private const float zombieSpeed = 3.0f;
    private const Race zombieRace = Race.Undead;
    private const float zombieMeleeCool = 1.0f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return zombieNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return zombieHealth; }
    }
    public override int NPCdefense
    {
        get { return zombieDefense; }
    }
    public override float NPCspeed
    {
        get { return zombieSpeed; }
    }
    public override Race race
    {
        get { return zombieRace; }
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

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= zombieMeleeRange)
        {
            if (zombieMeleeCool > MeleeCool) return;
            Target.Damage((int)(zombieAttack * friendlyAttackFactor));
            MeleeCool = 0;
        }
    }

    protected override void Init()
    {
        MeleeCool = 0;
        //skill = new Skill();
        unlock_cost = 0;
    }

    void Awake()
    {
        base.Awake();
    }

    //Update is called once per frame
    void Update()
    {
        base.Update();
        if (MeleeCool <= zombieMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return zombieMeleeRange;
    }
}
