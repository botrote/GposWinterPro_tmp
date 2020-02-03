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
    /// <summary>
    /// AI가 조작할 유닛 클래스.
    /// </summary>
    protected Unit body;
}
