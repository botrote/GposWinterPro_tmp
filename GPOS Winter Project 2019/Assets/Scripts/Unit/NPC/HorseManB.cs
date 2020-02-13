using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManB : NPC , IMissileAttack
{
    private const string unitname = "HorseManB";
    private const int HorseManBNotch = 1;
    private const int HorseManBExp = 1;
    private const int HorseManBHealth = 20;
    private const int HorseManBAttack = 10;
    private const int HorseManBDefense = 0;
    private const float HorseManBMissileRange = 5.0f;
    private const float HorseManBSpeed = 4.0f;
    private const Race HorseManBRace = Race.Soldier;
    private const float HorseManBMissileCool = 2.0f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return HorseManBNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return HorseManBHealth; }
    }
    public override int NPCdefense
    {
        get { return HorseManBDefense; }
    }
    public override float NPCspeed
    {
        get { return HorseManBSpeed; }
    }
    public override Race race
    {
        get { return HorseManBRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= HorseManBMissileRange)
        {
            if (HorseManBMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Arrow", this, this.position, Target.position, (int)HorseManBAttack, 10f, 1f);
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
    }

    void Awake()
    {
        base.Awake();
    }

    //Update is called once per frame
    void Update()
    {
        base.Update();
        if (MissileCool <= HorseManBMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return HorseManBMissileRange;
    }
}