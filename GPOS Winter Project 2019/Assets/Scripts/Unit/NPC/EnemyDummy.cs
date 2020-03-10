using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : NPC
{
    private const string unitname = "FriendlyDummy";
    private const int eDummyNotch = 0;
    private const int eDummyHealth = 99999999;
    private const int eDummyAttack = 0;
    private const int eDummyDefense = 0;
    private const float eDummyMissileRange = 0f;
    private const float eDummyEffectRadius = 3f;
    private const float eDummySpeed = 10f;
    private const Race eDummyRace = Race.None;
    private Vector2 origin;

    public override Team TeamTag
    {
        get { return Team.Enemy; }
    }
    public override int Notch
    {
        get { return eDummyNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return eDummyHealth; }
    }
    public override int NPCdefense
    {
        get { return eDummyDefense; }
    }
    public override float NPCspeed
    {
        get { return eDummySpeed; }
    }
    public override Race race
    {
        get { return eDummyRace; }
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

    }

    void Awake()
    {
        base.Awake();
        StartCoroutine(fixPos());
    }

    private IEnumerator fixPos()
    {
        yield return new WaitForEndOfFrame();
        origin = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        base.Update(); 
        transform.position = origin;
    }

    private void OnDestroy()
    {

    }

    public float getMissileRange()
    {
        return eDummyMissileRange;
    }
}
