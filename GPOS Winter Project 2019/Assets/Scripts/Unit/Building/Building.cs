﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Unit
{
    protected UnitFactoryManager factorymanager;
    protected Coroutine SpawnCoroutine;
    protected bool isInitialized;
    public override int MaxHealth
    {
        get { return MaxSpawn; }
    }
    public override int defense
    {
        get { return 1; }
    }
    public override Race race { get { return Race.None; } }
    public override Team TeamTag { get { return Team.Building; } }
    public override float speed
    {
        get { return 0; }
    }
    public override Vector2 Dest
    {
        get { return position; }
        set { }
    }
    public override string Unitname
    {
        get{ return "Building"; }
    }
    public int MaxSpawn { get; protected set; }
    public string Product { get; protected set; }
    public float SpawnDelay { get; protected set; }

    protected void Awake()
    {
        isInitialized = false;
        factorymanager = GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>();
    }

    protected void Update()
    {
    }

    public void Initialize(string _Product, int _MaxSpawn, float _SpawnDelay)
    {
        if (isInitialized) return;
        Product = _Product;
        MaxSpawn = _MaxSpawn;
        SpawnDelay = _SpawnDelay;
        //Debug.Log("Factory initialized : " + Product + "," + MaxSpawn + "," + SpawnDelay);
        base.Awake();
        isInitialized = true;
        SpawnCoroutine = StartCoroutine(SpawnEnemy());
    }

    public override void Damage(int damage)
    {
        return;
    }
    protected IEnumerator SpawnEnemy()
    {
        GameObject summoned;
        bool isLava = GameObject.Find("MapManager").GetComponent<MapManager>().isLava;
        while (curHealth>0)
        {
            yield return new WaitForSeconds(SpawnDelay);
            summoned = factorymanager.PlaceUnit(Product, this.position + new Vector2(0, 0));
            if(isLava)
                StartCoroutine(GameObject.Find("Manager").GetComponent<EffectManager>().BuildLavaEnemySpawn(summoned));
            else
                StartCoroutine(GameObject.Find("Manager").GetComponent<EffectManager>().BuildGrassEnemySpawn(summoned));
            StartCoroutine(EnableRenderer(summoned));
            curHealth--;
        }
        Die();
    }

    private IEnumerator EnableRenderer(GameObject summoned)
    {
        yield return new WaitForSeconds(0.3f);
        summoned.GetComponent<SpriteRenderer>().enabled = true;
    }
}
