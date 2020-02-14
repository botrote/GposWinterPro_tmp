using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBoss : NPC , IMissileAttack
{
    private const string unitname = "SniperBoss";
    private const int SniperBossNotch = 1;
    private const int SniperBossExp = 1;
    private const int SniperBossHealth = 300;
    private const int SniperBossAttack = 50;
    private const int SniperBossDefense = 0;
    private const float SniperBossMissileRange = 20.0f;
    private const float SniperBossSpeed = 7.0f;
    private const Race SniperBossRace = Race.Elite;
    private const float SniperBossMissileCool = 6.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return SniperBossNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return SniperBossHealth; }
    }
    public override int NPCdefense
    {
        get { return SniperBossDefense; }
    }
    public override float NPCspeed
    {
        get { return SniperBossSpeed; }
    }
    public override Race race
    {
        get { return SniperBossRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= SniperBossMissileRange)
        {
            if (SniperBossMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Bolt", this, this.position, Target.position, (int)SniperBossAttack, 15f, 1f);
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
        StartCoroutine(Check());
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
        if (MissileCool <= SniperBossMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return SniperBossMissileRange;
    }
    protected IEnumerator Check()
    {
        yield return null;
        while(this.curHealth>this.MaxHealth*0.75f) yield return null;
        Teleport();
        while(this.curHealth>this.MaxHealth*0.5f) yield return null;
        Teleport();
        while(this.curHealth>this.MaxHealth*0.25f) yield return null;
        Teleport();
        
    }
    protected void Teleport()
    {
        float theta;
        theta=Random.Range(0, 2*Mathf.PI);
        transform.position=GameObject.Find("Player").GetComponent<Player>().position+(new Vector2(Mathf.Cos(theta) ,Mathf.Sin(theta)))*20;
    }
}