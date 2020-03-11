using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichKing : NPC , IMissileAttack
{
    private const string unitname = "LichKing";
    private const int LichKingNotch = 40;
    private const int LichKingHealth = 100;
    private const int LichKingAttack = 20;
    private const int LichKingDefense = 5;
    private const float LichKingMissileRange = 7.0f;
    private const float LichKingSpeed = 5.0f;
    private const Race LichKingRace = Race.Undead;
    private const float LichKingMissileCool = 1.0f;
    private const float LichKingSummonCool = 3.0f;
    private float MissileCool;
    private float SummonCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return LichKingNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return LichKingHealth; }
    }
    public override int NPCdefense
    {
        get { return LichKingDefense; }
    }
    public override float NPCspeed
    {
        get { return LichKingSpeed; }
    }
    public override Race race
    {
        get { return LichKingRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= LichKingMissileRange)
        {
            if (LichKingMissileCool > MissileCool) return;
            GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Death", this, this.position, Target.position, (int)(LichKingAttack*friendlyAttackFactor), 10f, 1f);
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
        unlock_cost = 200;
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
        if (MissileCool <= LichKingMissileCool)
        {
            MissileCool += Time.deltaTime;
        }
        if (SummonCool <= LichKingSummonCool)
        {
            SummonCool += Time.deltaTime;
            if (SummonCool>=LichKingSummonCool)
            {
                float theta;
                Vector2 dest;
                do
                {
                    theta=Random.Range(0, 2*Mathf.PI);
                    dest=this.position+(new Vector2(Mathf.Cos(theta) ,Mathf.Sin(theta)))*1.5f;
                }
                while(GameObject.Find("MapManager").GetComponent<MapManager>().IsOutOfBoundary(dest));
                GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>().PlaceUnit("Zombie", dest);
                SummonCool = 0;
            }
        }
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return LichKingMissileRange;
    }
}