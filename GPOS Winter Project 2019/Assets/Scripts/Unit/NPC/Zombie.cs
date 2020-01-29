using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : NPC , IMeleeAttack
{
    private static ushort zombieNumber = 0;
    private const ushort zombieNotch = 1;
    private const ushort zombieHealth = 50;
    private const ushort zombieAttack = 5;
    private const ushort zombieDefense = 1;
    private const float zombieRange = 0;
    private const float zombieSpeed = 1.0f;

    public override ushort Notch
    {
        get { return zombieNotch; }
    }

    public void MeleeAttack(Unit Target)
    {

    }

    protected virtual void Init()
    {
        ai = new AI();
        skill = new Skill();
        maxHealth = zombieHealth;
        curHealth = maxHealth;
        attack = zombieAttack;
        defense = zombieDefense;
        range = zombieRange;
        speed = zombieSpeed;
        race = Race.Undead;
        zombieNumber++;
    }

    void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        zombieNumber--;
    }

    

    public override int getNumofUnit()
    {
        return zombieNumber;
    }
}
