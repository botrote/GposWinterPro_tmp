using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int wave;
    private UnitFactoryManager factorymanager;
    private Coroutine WaveCoroutine;
    private bool WaveWaiting;
    public int getWave
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
        factorymanager = GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>();
    }

    private void Start()
    {
        if (factorymanager.isFactoryLoaded)
        {
            //factorymanager.PlaceUnit("LichKing", new Vector2(2,4));
            //factorymanager.PlaceUnit("Ghost", new Vector2(0,4));
            //factorymanager.PlaceUnit("Ghost", new Vector2(4, 6));
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
        factorymanager.PlaceUnit("Building", new Vector2(10, 0), "Soldier", (int)1, 5f);
        factorymanager.PlaceUnit("Building", new Vector2(-10, 4), "Hero", (int)1, 5f);
        factorymanager.PlaceUnit("Building", new Vector2(-10, -4), "Soldier", (int)1, 5f);
    }
}
