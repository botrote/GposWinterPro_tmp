using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제로 유닛 게임오브젝트를 생성하는 클래스. 팩토리 오브젝트에 컴포넌트로 붙여 놓을 것.
/// </summary>
public abstract class ProjectileFactory : MonoBehaviour
{
    protected void Awake()
    {
        ProjectileFactoryManager.factoryDict.Add(this.Product, this);
    }
    /// <summary>
    /// 이 팩토리가 생성할 수 있는 유닛의 이름(Unit의 Unitname과 일치해야 함)
    /// </summary>
    public abstract string Product { get; }
    /// <summary>
    /// 투사체 생성해서 반환
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    /// <param name="duration"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public abstract GameObject MakeProjectile(int damage, float duration, Unit.Team team, params object[] parameter);
    protected void OnDestroy()
    {
        ProjectileFactoryManager.factoryDict.Remove(this.Product);
    }
}
