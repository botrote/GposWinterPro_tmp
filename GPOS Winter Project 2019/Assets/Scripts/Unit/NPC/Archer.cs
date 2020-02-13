using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : NPC , IMissileAttack
{
    private const string unitname = "Archer";
    private const int ArcherNotch = 1;
    private const int ArcherExp = 1;
    private const int ArcherHealth = 20;
    private const int ArcherAttack = 10;
    private const int ArcherDefense = 0;
    private const float ArcherMissileRange = 5.0f;
    private const float ArcherSpeed = 4.0f;
    private const Race ArcherRace = Race.Soldier;
    private const float ArcherMissileCool = 2.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return ArcherNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return ArcherHealth; }
    }
    public override int NPCdefense
    {
        get { return ArcherDefense; }
    }
    public override float NPCspeed
    {
        get { return ArcherSpeed; }
    }
    public override Race race
    {
        get { return ArcherRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= ArcherMissileRange)
        {
            if (ArcherMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Arrow", this, this.position, Target.position, (int)ArcherAttack, 10f, 1f);
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
        unlock_cost = int.MaxValue;
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
        if (MissileCool <= ArcherMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return ArcherMissileRange;
    }
}