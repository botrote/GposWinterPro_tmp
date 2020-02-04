using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Unit
{
    protected FactoryManager factorymanager;
    protected Coroutine SpawnCoroutine;
    protected bool isInitialized;
    public override uint MaxHealth
    {
        get { return MaxSpawn; }
    }
    public override uint defense
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
    public uint MaxSpawn { get; protected set; }
    public string Product { get; protected set; }
    public float SpawnDelay { get; protected set; }

    protected void Awake()
    {
        isInitialized = false;
        factorymanager = GameObject.Find("UnitFactory").GetComponent<FactoryManager>();
    }

    protected void Update()
    {
    }

    public void Initialize(string _Product, uint _MaxSpawn, float _SpawnDelay)
    {
        if (isInitialized) return;
        Product = _Product;
        MaxSpawn = _MaxSpawn;
        SpawnDelay = _SpawnDelay;
        Debug.Log("Factory initialized : " + Product + "," + MaxSpawn + "," + SpawnDelay);
        base.Awake();
        isInitialized = true;
        SpawnCoroutine = StartCoroutine(SpawnEnemy());
    }

    public override void Damage(uint damage)
    {
        return;
    }
    protected IEnumerator SpawnEnemy()
    {
        while (curHealth>0)
        {
            Debug.Log("Waiting");
            yield return new WaitForSeconds(SpawnDelay);
            Debug.Log("Spawning"+ Product);
            factorymanager.PlaceUnit(Product, this.position + new Vector2(2, 0));
            curHealth--;
            Debug.Log(curHealth);
        }
        Die();
    }
}
