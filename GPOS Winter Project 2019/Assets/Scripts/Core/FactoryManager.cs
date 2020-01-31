using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public bool isFactoryLoaded
    {
        get;
        private set;
    }
    public static Dictionary<string, UnitFactory> factoryDict = new Dictionary<string, UnitFactory>();
    // Start is called before the first frame update
    void Awake()
    {
        //factoryDict = new Dictionary<string, UnitFactory>();
        //UnitFactory[] factories = gameObject.GetComponents<UnitFactory>();
        //for(int i = 0; i<factories.Length; i++)
        //{
        //    factoryDict.Add(factories[i].Product, factories[i]);
        //}
        isFactoryLoaded = true;
    }
    public GameObject PlaceUnit(string name, Vector2 pos, params object[] parameter)
    {
        UnitFactory factory;
        if (factoryDict.TryGetValue(name, out factory))
        {
            GameObject product = factory.MakeUnit(parameter);
            product.GetComponent<Transform>().position = pos;
            return product;
        }
        else
        {
            Debug.Log("Found no factory able to produce" + name);
            return null;
        }
    }
}
