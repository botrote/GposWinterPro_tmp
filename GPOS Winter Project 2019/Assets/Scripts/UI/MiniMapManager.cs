using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    GameObject dot;
    void Awake()
    {
        dot = Resources.Load("Prefabs/dot") as GameObject;
        Unit.UnitSpawnEvent += CreateDot;
    }

    void CreateDot(Unit _unit)
    {
        GameObject instanceDot = Instantiate(dot);
        instanceDot.transform.parent = gameObject.transform;
        instanceDot.GetComponent<MinimapDot>().setUnit(_unit);
    }
}
