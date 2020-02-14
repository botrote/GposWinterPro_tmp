using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : NPC, IMeleeAttack
{
    protected UnitFactoryManager factoryManager;
    private const string unitname = "Hero";
    private const int HeroNotch = 1;
    private const int HeroExp = 0;
    private const int HeroHealth = 1000;
    private const int HeroAttack = 40;
    private const int HeroDefense = 20;
    private const float HeroMeleeRange = 1.0f;
    private const float HeroSpeed = 7.0f;
    private const Race HeroRace = Race.Hero;
    private const float HeroMeleeCool = 1f;
    private float MeleeCool;

    private const float HeroFistCool = 3f;
    private const int FistDamage = 20;
    private float FistCool;

    private const float HeroChargeCool = 5f;
    private float ChargeCool;

    private const float HeroTeleportCool = 3f;
    private const int TeleportDamage = 30;
    private const float TeleportDamageRadius = 1.5f;
    private float TeleportCool;

    private const float HeroBeamCool = 5f;
    private const int BeamDamage = 40;
    private float BeamCool;

    private const float HeroSummonCool = 15f;
    private float SummonCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return HeroNotch; }
    }
    protected override float RateOfSpecialAttack
    {
        get { return 0; }
    }
    public override int NPCMaxHealth
    {
        get { return HeroHealth; }
    }
    public override int NPCdefense
    {
        get { return HeroDefense; }
    }
    public override float NPCspeed
    {
        get { return HeroSpeed; }
    }
    public override Race race
    {
        get { return HeroRace; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }

    public override int Exp
    {
        get { return HeroExp; }
    }

    public void MeleeAttack(Unit Target)
    {
        if (isStunned||Target==null) return;
        if ( Vector2.Distance(Target.position, this.position) <= HeroMeleeRange)
        {
            if (HeroMeleeCool > MeleeCool) return;
            Target.Damage(HeroAttack);
            MeleeCool = 0;
        }
    }
    public void FistAttack(Unit Target)
    {
        if (isStunned||Target==null) return;
        if (HeroFistCool > FistCool) return;
        GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Fist", this, this.position, Target.position, (int)(FistDamage), 10f, 0.5f);
        FistCool=0;
    }
    public void ChargeAttack(Unit Target)
    {
        if (isStunned||Target==null) return;
        if (HeroChargeCool > ChargeCool) return;
        float theta;
        Vector2 dest;
        do
        {
            theta=Random.Range(0, 2*Mathf.PI);
            dest=Target.position+(new Vector2(Mathf.Cos(theta) ,Mathf.Sin(theta)));
        }
        while(GameObject.Find("MapManager").GetComponent<MapManager>().IsOutOfBoundary(dest));
        transform.position=dest;
        ChargeCool=0;
    }
    public void TeleportAttack(Unit Target)
    {
        if (isStunned||Target==null) return;
        if (HeroTeleportCool > TeleportCool) return;
        float theta;
        Vector2 dest;
        do
        {
            theta=Random.Range(0, 2*Mathf.PI);
            dest=Target.position+(new Vector2(Mathf.Cos(theta) ,Mathf.Sin(theta)));
        }
        while(GameObject.Find("MapManager").GetComponent<MapManager>().IsOutOfBoundary(dest));
        transform.position=dest;

        Collider2D[] Targets = Physics2D.OverlapCircleAll(Target.position, TeleportDamageRadius);
        for(int i=0; i<Targets.Length; i++)
        {
            if(Targets[i].gameObject.GetComponent<Unit>()==null) continue;
            else if ( Targets[i].gameObject.GetComponent<Unit>().tag.Equals("Friendly"))
            {
                Targets[i].gameObject.GetComponent<Unit>().Damage(TeleportDamage);
            }
        }
        TeleportCool=0;
    }
    public void BeamAttack(Unit Target)
    {
        if (isStunned||Target==null||this.curHealth>this.MaxHealth/2) return;
        if (HeroBeamCool > BeamCool) return;
        GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Beam", this, this.position, Target.position, (int)(BeamDamage), 16f, 10f);
        BeamCool=0;
    }
    protected override void Init()
    {
        MeleeCool = 0;
        FistCool=0;
        ChargeCool=0;
        TeleportCool=0;
        BeamCool=0;
        //skill = new Skill();
        unlock_cost = int.MaxValue;
    }

    void Awake()
    {
        base.Awake();
        factoryManager = GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(MeleeCool <= HeroMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
        if(FistCool <= HeroFistCool)
        {
            FistCool += Time.deltaTime;
        }
        if(ChargeCool <= HeroChargeCool)
        {
            ChargeCool += Time.deltaTime;
        }
        if(TeleportCool <= HeroTeleportCool)
        {
            TeleportCool += Time.deltaTime;
        }
        if(BeamCool <= HeroBeamCool)
        {
            BeamCool += Time.deltaTime;
        }
        if(SummonCool <= HeroSummonCool)
        {
            SummonCool += Time.deltaTime;
        }
        Summon();
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return HeroMeleeRange;
    }

    private void Summon()
    {
        if (HeroSummonCool > SummonCool) return;
        int portal=Random.Range(0, 8);
        switch(Random.Range(1, 6))
        {
            default:
            case 1:
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "Cleric", 5, 0.2f);
            break;
            case 2:
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "HorseManL", 3, 0.2f);
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "HorseManB", 3, 0.2f);
            break;
            case 3:
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "Soldier", 20, 0.2f);
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "Archer", 15, 0.2f);
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "Cleric", 1, 0.2f);
            break;
            case 4:
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "Knight", 5, 0.2f);
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "Arms", 3, 0.2f);
            break;
            case 5:
            factoryManager.PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[portal], "Sniper", 5, 0.2f);
            break;
        }
        SummonCool=0;
    }
}
