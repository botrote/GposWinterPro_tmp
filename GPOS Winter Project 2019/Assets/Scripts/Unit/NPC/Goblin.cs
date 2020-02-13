using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : NPC , IMissileAttack
{
    private const string unitname = "Goblin";
    private const int GoblinNotch = 2;
    private const int GoblinHealth = 20;
    private const int GoblinAttack = 20;
    private const int GoblinDefense = 0;
    private const float GoblinMissileRange = 3.5f;
    private const float GoblinDamageRadius = 1.5f;
    private const float GoblinSpeed = 3.0f;
    private const Race GoblinRace = Race.None;
    private const float GoblinMissileCool = 2.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return GoblinNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return GoblinHealth; }
    }
    public override int NPCdefense
    {
        get { return GoblinDefense; }
    }
    public override float NPCspeed
    {
        get { return GoblinSpeed; }
    }
    public override Race race
    {
        get { return GoblinRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= GoblinMissileRange)
        {
            if (GoblinMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Explosive", this, this.position, Target.position, GoblinAttack, 10f, 1f, GoblinDamageRadius);
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
        unlock_cost = 70;
    }

    void Awake()
    {
        base.Awake();
    }

    //Update is called once per frame
    void Update()
    {
        base.Update();
        if (MissileCool <= GoblinMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return GoblinMissileRange;
    }
}