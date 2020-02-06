using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : NPC , IMeleeAttack
{
    private const string unitname = "Zombie";
    private const uint zombieNotch = 1;
    private const uint zombieHealth = 50;
    private const uint zombieAttack = 5;
    private const uint zombieDefense = 1;
    private const float zombieMeleeRange = 3.0f;
    private const float zombieSpeed = 1.0f;
    private const Race zombieRace = Race.Undead;
    private const float zombieMeleeCool = 3.0f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint Notch
    {
        get { return zombieNotch; }
    }
    public override uint NPCMaxHealth
    {
        get { return zombieHealth; }
    }
    public override uint NPCdefense
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

    public override uint Exp
    {
        get { return 0; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (Vector2.Distance(Target.position, this.position) <= zombieMeleeRange)
        {
            if (zombieMeleeCool > MeleeCool) return;
            Target.Damage((uint)(zombieAttack * friendlyAttackFactor));
            MeleeCool = 0;
        }
    }

    protected override void Init()
    {
        MeleeCool = 0;
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
