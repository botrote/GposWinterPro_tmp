using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonS : NPC , IMeleeAttack
{
    private const string unitname = "SkeletonS";
    private const int SkeletonSNotch = 1;
    private const int SkeletonSHealth = 30;
    private const int SkeletonSAttack = 10;
    private const int SkeletonSDefense = 0;
    private const float SkeletonSMeleeRange = 1.0f;
    private const float SkeletonSSpeed = 5.0f;
    private const Race SkeletonSRace = Race.Undead;
    private const float SkeletonSMeleeCool = 1.0f;
    private float MeleeCool;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return SkeletonSNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return SkeletonSHealth; }
    }
    public override int NPCdefense
    {
        get { return SkeletonSDefense; }
    }
    public override float NPCspeed
    {
        get { return SkeletonSSpeed; }
    }
    public override Race race
    {
        get { return SkeletonSRace; }
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
        if (Vector2.Distance(Target.position, this.position) <= SkeletonSMeleeRange)
        {
            if (SkeletonSMeleeCool > MeleeCool) return;
            Target.Damage((int)(SkeletonSAttack * friendlyAttackFactor));
            MeleeCool = 0;
        }
    }

    protected override void Init()
    {
        MeleeCool = 0;
        //skill = new Skill();
        unlock_cost = 0;
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
        if (MeleeCool <= SkeletonSMeleeCool)
        {
            MeleeCool += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {

    }

    public float getMeleeRange()
    {
        return SkeletonSMeleeRange;
    }
}