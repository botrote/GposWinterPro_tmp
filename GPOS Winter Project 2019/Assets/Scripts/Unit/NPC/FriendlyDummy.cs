using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyDummy : NPC
{
    private const string unitname = "FriendlyDummy";
    private const int fDummyNotch = 0;
    private const int fDummyHealth = 99999999;
    private const int fDummyAttack = 0;
    private const int fDummyDefense = 0;
    private const float fDummyMissileRange = 0f;
    private const float fDummyEffectRadius = 3f;
    private const float fDummySpeed = 10f;
    private const Race fDummyRace = Race.None;
    private Vector2 origin;

    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int Notch
    {
        get { return fDummyNotch; }
    }
    public override int NPCMaxHealth
    {
        get { return fDummyHealth; }
    }
    public override int NPCdefense
    {
        get { return fDummyDefense; }
    }
    public override float NPCspeed
    {
        get { return fDummySpeed; }
    }
    public override Race race
    {
        get { return fDummyRace; }
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
        return fDummyMissileRange;
    }
}
