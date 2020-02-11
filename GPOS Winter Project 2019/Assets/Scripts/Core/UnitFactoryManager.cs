using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유닛 팩토리 매니저, 유닛을 생성할 때에는 항상 이 클래스에 요청해야 함.
/// </summary>
public class UnitFactoryManager : MonoBehaviour
{
    /// <summary>
    /// 팩토리 객체가 모두 추가되면 true여야 함
    /// </summary>
    public bool isFactoryLoaded
    {
        get;
        private set;
    }
    /// <summary>
    /// 씬 내에 있는 유닛팩토리의 목록, 유닛을 생성할 때 여기에서 검색하여 사용함
    /// </summary>
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
    /// <summary>
    /// 유닛을 맵상의 특정 위치에 소환하고 해당 유닛의 게임오브젝트를 반환함
    /// </summary>
    /// <param name="name">유닛의 종류(유닛팩토리의 Product와 일치해야 함)</param>
    /// <param name="pos">유닛을 생성할 위치</param>
    /// <param name="parameter">유닛의 생성 매개변수, 여러 종류의 매개변수에 대응하기 위해 가변 오브젝트 배열 사용</param>
    /// <returns>유닛 게임 오브젝트</returns>
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
