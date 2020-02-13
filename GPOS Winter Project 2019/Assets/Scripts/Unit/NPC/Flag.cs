using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : NPC
{
    private const string unitname = "Flag";
    private const int FlagNotch = 4;
    private const int FlagHealth = 30;
    private const int FlagAttack = 0;
    private const int FlagDefense = 0;
    private const int FlagSelfDmg = 2;
    private const float FlagMissileRange = 0f;
    private const float FlagEffectRadius = 5f;
    private const float FlagSpeed = 0f;
    private const Race FlagRace = Race.None;
    private const float FlagEffectCool = 0.2f;
    private const float FlagSelfDmgCool = 1f;
    private float EffectCool;
    private float SelfDmgCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return FlagNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return FlagHealth; }
    }
    public override int NPCdefense
    {
        get { return FlagDefense; }
    }
    public override float NPCspeed
    {
        get { return FlagSpeed; }
    }
    public override Race race
    {
        get { return FlagRace; }
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

    protected override void Init()
    {
        EffectCool = 0;
        SelfDmgCool = 0;
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
        if (EffectCool <= FlagEffectCool)
        {
            EffectCool += Time.deltaTime;
        }
        if (EffectCool>=FlagEffectCool)
        {
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Curse", this, this.position, this.position, 0, 0f, 0.2f, FlagEffectRadius);
            EffectCool = 0;
        }

        if ( SelfDmgCool<= FlagSelfDmgCool)
        {
            SelfDmgCool += Time.deltaTime;
        }
        if (SelfDmgCool>=FlagSelfDmgCool)
        {
            this.Damage(FlagSelfDmg);
            SelfDmgCool = 0;
        }
            
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return FlagMissileRange;
    }
}