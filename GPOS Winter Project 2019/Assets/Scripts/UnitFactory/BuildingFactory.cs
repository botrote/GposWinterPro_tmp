using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BuildingFactory : UnitFactory
{
    private const string product = "Building";
    public override string Product { get { return product; } }
    protected GameObject template;

    protected void Awake()
    {
        base.Awake();
        template = Resources.Load("Prefabs/Portal") as GameObject;
    }

    public override GameObject MakeUnit(params object[] parameter)
    {
        for(int i = 0; i < parameter.Length; i++)
        {
            Debug.Log(parameter[i]);
        }
        GameObject Instance = GameObject.Instantiate(template);
        Instance.GetComponent<Building>().Initialize((string)parameter[0], (int)parameter[1], (float)parameter[2]);
        return Instance;
    }
}

