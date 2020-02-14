using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 투사체 팩토리 매니저, 투사체를 생성할 때에는 항상 이 클래스에 요청해야 함.
/// </summary>
public class ProjectileFactoryManager : MonoBehaviour
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
    /// 씬 내에 있는 투사체 팩토리의 목록, 투사체를 생성할 때 여기에서 검색하여 사용함
    /// </summary>
    public static Dictionary<string, ProjectileFactory> factoryDict = new Dictionary<string, ProjectileFactory>();
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
    public GameObject PlaceProjectile(string name, Unit Shooter, Vector2 pos, Vector2 target, int damage, float speed, float duration, params object[] parameter)
    {
        ProjectileFactory factory;
        if (factoryDict.TryGetValue(name, out factory))
        {
            GameObject product = factory.MakeProjectile(damage, duration, Shooter.TeamTag, parameter);
            product.GetComponent<Transform>().position = pos;
            Vector2 dir = (target - pos).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            product.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if(product.GetComponent<Rigidbody2D>() != null) product.GetComponent<Rigidbody2D>().velocity = (target - pos).normalized * speed;
            return product;
        }
        else
        {
            Debug.Log("Found no factory able to produce " + name);
            return null;
        }
    }
}
