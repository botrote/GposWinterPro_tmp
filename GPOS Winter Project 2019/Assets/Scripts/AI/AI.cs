using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI 추상 클래스, 딱히 기능은 없음.
/// 모든 AI는 게임 오브젝트에 컴포넌트로 추가해야 함(프리팹 이용)
/// </summary>
public abstract class AI : MonoBehaviour
{
    public const float MaxDist = 5f;
    public const float MaxBattleDist = 10f;
    /// <summary>
    /// AI가 조작할 유닛 클래스.
    /// </summary>
    protected Unit body;
    protected Unit Target;
    protected virtual Unit FindTarget(String TargetTag, float Sight=7f)
    {
        Collider2D[] Targets = Physics2D.OverlapCircleAll(body.position, Sight);
        Unit CurTarget=null;
        for(int i=0; i<Targets.Length; i++)
        {
            if(Targets[i]==null) break;
            else if(!Targets[i].tag.Equals(TargetTag)) continue;
            else if(CurTarget==null||CurTarget==body) CurTarget=Targets[i].gameObject.GetComponent<Unit>();
            else if(Vector2.Distance(CurTarget.position, body.position)>Vector2.Distance(Targets[i].gameObject.GetComponent<Unit>().position, body.position))
            {
                CurTarget=Targets[i].gameObject.GetComponent<Unit>();
            }    
        }
        if(CurTarget==body) return null;
        return CurTarget;
    }
}
