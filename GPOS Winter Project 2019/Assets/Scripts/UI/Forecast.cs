using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forecast : MonoBehaviour
{
    protected void Awake()
    {
        GameObject.Find("Manager").GetComponent<WaveManager>().SpawnForecast += this.show;
    }

    protected void show(int portalnum, string name)
    {

    }
}
