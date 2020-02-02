using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제로 유닛 게임오브젝트를 생성하는 클래스. 팩토리 오브젝트에 컴포넌트로 붙여 놓을 것.
/// </summary>
public abstract class UnitFactory : MonoBehaviour
{
    protected void Awake()
    {
        FactoryManager.factoryDict.Add(this.Product, this);
    }
    /// <summary>
    /// 이 팩토리가 생성할 수 있는 유닛의 이름(Unit의 Unitname과 일치해야 함)
    /// </summary>
    public abstract string Product { get; }
    /// <summary>
    /// 유닛을 생성하고 게임오브젝트 반환(팩토리매니저가 위치를 세팅함)
    /// </summary>
    /// <param name="parameter">유닛 생성 매개변수, 오브젝트 배열이므로 언박싱해서 사용해야 함</param>
    /// <returns>유닛 게임 오브젝트</returns>
    public abstract GameObject MakeUnit(params object[] parameter);
    protected void OnDestroy()
    {
        FactoryManager.factoryDict.Remove(this.Product);
    }
}
