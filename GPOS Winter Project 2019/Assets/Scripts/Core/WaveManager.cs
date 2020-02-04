using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private uint wave;
    private FactoryManager factorymanager;
    public uint getWave
    {
        get { return wave; }
    }

    // Start is called before the first frame update
    void Reset()
    {
        wave = 1;
    }

    void Awake()
    {
        factorymanager = GameObject.Find("UnitFactory").GetComponent<FactoryManager>();
    }

    private void Start()
    {
        if (factorymanager.isFactoryLoaded)
        {
            Spawnwave();
        }
    }

    // Update is called once per frame

    private void Spawnwave()
    {
        factorymanager.PlaceUnit("Zombie", new Vector2(0, 0));
        factorymanager.PlaceUnit("Zombie", new Vector2(3, 0));
        factorymanager.PlaceUnit("Zombie", new Vector2(5, 0));
        factorymanager.PlaceUnit("Soldier", new Vector2(0, 5));
        factorymanager.PlaceUnit("Building", new Vector2(-10,0),"Soldier", (uint)5 , 5.0f);
    }
}
