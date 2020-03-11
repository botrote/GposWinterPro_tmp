using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : NPC , IMissileAttack
{
    private const string unitname = "Lich";
    private const int LichNotch = 4;
    private const int LichHealth = 30;
    private const int LichAttack = 15;
    private const int LichDefense = 0;
    private const float LichMissileRange = 5.0f;
    private const float LichDamageRadius = 1.0f;
    private const float LichSpeed = 5.0f;
    private const Race LichRace = Race.Undead;
    private const float LichMissileCool = 1.2f;
    private float MissileCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return LichNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return LichHealth; }
    }
    public override int NPCdefense
    {
        get { return LichDefense; }
    }
    public override float NPCspeed
    {
        get { return LichSpeed; }
    }
    public override Race race
    {
        get { return LichRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= LichMissileRange)
        {
            if (LichMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("DeathBall", this, Target.position, Target.position, (int)(LichAttack*friendlyAttackFactor), 0f, 0.5f, LichDamageRadius);
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
        unlock_cost = 100;
    }

    void Awake()
    {
        base.Awake();
        StartCoroutine(SummonEffect());
    }

    IEnumerator SummonEffect()
    {
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(GameObject.Find("Manager").GetComponent<EffectManager>().BuildFriendlySpawn(gameObject));
        yield return new WaitForSeconds(0.35f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    //Update is called once per frame
    void Update()
    {
        base.Update();
        if (MissileCool <= LichMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return LichMissileRange;
    }
}