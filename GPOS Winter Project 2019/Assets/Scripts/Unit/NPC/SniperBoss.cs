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
    UnitFactoryManager factorymanager;

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
        factorymanager = GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>();
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
        {
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[1], "Soldier", 20, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[1], "Archer", 10, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[1], "Cleric", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[3], "Soldier", 20, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[3], "Archer", 10, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[3], "Cleric", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[4], "Soldier", 20, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[4], "Archer", 10, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[4], "Cleric", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[6], "Soldier", 20, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[6], "Archer", 10, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[6], "Cleric", 2, 0.2f);
        }
        Teleport();
        while(this.curHealth>this.MaxHealth*0.5f) yield return null;
        {
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[0], "Knight", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[0], "Soldier", 10, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[0], "Cleric", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[0], "HorseManB", 3, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[2], "Knight", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[2], "Soldier", 10, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[2], "Cleric", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[2], "HorseManB", 3, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[5], "Knight", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[5], "Soldier", 10, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[5], "Cleric", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[5], "HorseManB", 3, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[7], "Knight", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[7], "Soldier", 10, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[7], "Cleric", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[7], "HorseManB", 3, 0.2f);
        }
        Teleport();
        while(this.curHealth>this.MaxHealth*0.25f) yield return null;
        {
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[0], "Arms", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[0], "Spearman", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[0], "HorseManL", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[1], "Arms", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[1], "Spearman", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[1], "HorseManL", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[2], "Arms", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[2], "Spearman", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[2], "HorseManL", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[3], "Arms", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[3], "Spearman", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[3], "HorseManL", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[4], "Arms", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[4], "Spearman", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[4], "HorseManL", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[5], "Arms", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[5], "Spearman", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[5], "HorseManL", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[6], "Arms", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[6], "Spearman", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[6], "HorseManL", 2, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[7], "Arms", 1, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[7], "Spearman", 5, 0.2f);
            factorymanager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[7], "HorseManL", 2, 0.2f);
        }
        Teleport();
        
    }
    protected void Teleport()
    {
        float theta;
        theta=Random.Range(0, 2*Mathf.PI);
        transform.position=GameObject.Find("Player").GetComponent<Player>().position+(new Vector2(Mathf.Cos(theta) ,Mathf.Sin(theta)))*20;
    }
}