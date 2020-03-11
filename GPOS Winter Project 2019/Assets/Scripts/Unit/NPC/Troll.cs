using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : NPC , IMeleeAttack
{
    private const string unitname = "Troll";
    private const int TrollNotch = 2;
    private const int TrollHealth = 20;
    private const int TrollAttack = 10;
    private const int TrollDefense = 0;
    private const float TrollMeleeRange = 1.0f;
    private const float TrollSpeed = 5.0f;
    private const Race TrollRace = Race.None;
    private const float TrollMeleeCool = 0.7f;
    private float MeleeCool;
    private const float TrollHealCool = 1.0f;
    private float HealCool = 0.0f;
    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return TrollNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return TrollHealth; }
    }
    public override int NPCdefense
    {
        get { return TrollDefense; }
    }
    public override float NPCspeed
    {
        get { return TrollSpeed; }
    }
    public override Race race
    {
        get { return TrollRace; }
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

    public void MeleeAttack(Unit Target)
    {
        if (isStunned) return;
        if (Vector2.Distance(Target.position, this.position) <= TrollMeleeRange)
        {
            if (TrollMeleeCool > MeleeCool) return;
            StartCoroutine(GameObject.Find("Manager").GetComponent<EffectManager>().BuildFriendlyScissor(gameObject, Target.position));
            Target.Damage((int)(TrollAttack * friendlyAttackFactor));
            MeleeCool = 0;
            if(Random.Range(0f,1.0f)<=0.2f) Target.Addbuff(new Stun());
        }
    }

    protected override void Init()
    {
        MeleeCool = 0;
        //skill = new Skill();
        unlock_cost = 50;
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
        if (MeleeCool <= TrollMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }

        if (HealCool <= TrollHealCool)
        {
            HealCool += Time.deltaTime;
        }
        else
        {
            Heal(MaxHealth/4);
            HealCool=0;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return TrollMeleeRange;
    }
}