using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : NPC , IMissileAttack
{
    private const string unitname = "Arms";
    private const int ArmsNotch = 4;
    private const int ArmsHealth = 100;
    private const int ArmsAttack = 15;
    private const int ArmsDefense = 40;
    private const float ArmsMissileRange = 1.2f;
    private const float ArmsDamageRadius = 0.5f;
    private const float ArmsSpeed = 1.0f;
    private const Race ArmsRace = Race.Soldier;
    private const float ArmsMissileCool = 2f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return ArmsNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return ArmsHealth; }
    }
    public override int NPCdefense
    {
        get { return ArmsDefense; }
    }
    public override float NPCspeed
    {
        get { return ArmsSpeed; }
    }
    public override Race race
    {
        get { return ArmsRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= ArmsMissileRange)
        {
            if (ArmsMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Crack", this, Target.position, Target.position, (int)ArmsAttack, 0f, 1f, ArmsDamageRadius);
            StartCoroutine(GameObject.Find("Manager").GetComponent<EffectManager>().BuildEnemySmite(gameObject, Target.position));
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
        if (MissileCool <= ArmsMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return ArmsMissileRange;
    }
}