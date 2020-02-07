using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private uint wave;
    private FactoryManager factorymanager;
    private Coroutine WaveCoroutine;
    private bool WaveWaiting;
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
            factorymanager.PlaceUnit("SkeletonS", new Vector2(2, 0));
            factorymanager.PlaceUnit("SkeletonB", new Vector2(4, 2));
            factorymanager.PlaceUnit("Ghost", new Vector2(4, 6));
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Building").Length == 0)
        {
            if (factorymanager.isFactoryLoaded && !WaveWaiting)
            {
                Debug.Log("Next wave in 10 seconds");
                WaveCoroutine = StartCoroutine(SpawnwaveLater(10));
            }
        }
    }
    
    private IEnumerator SpawnwaveLater(float second)
    {
        WaveWaiting = true;
        yield return new WaitForSeconds(second);
        Spawnwave();
        WaveWaiting = false;
        yield return null;
    }

    private void Spawnwave()
    {
        factorymanager.PlaceUnit("Building", new Vector2(10, 0), "Soldier", (uint)1, 5f);
        factorymanager.PlaceUnit("Building", new Vector2(-10, 4), "Soldier", (uint)1, 5f);
        factorymanager.PlaceUnit("Building", new Vector2(-10, -4), "Soldier", (uint)1, 5f);
    }
}
