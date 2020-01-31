using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : Unit
{
    public abstract ushort Notch
    {
        get;
    }

    protected ushort exp;
    //protected Skill skill;
    protected AI ai;
    protected abstract float RateOfSpecialAttack { get; }
    
    /// <summary>
    /// AI, Skill 초기화
    /// </summary>
    protected abstract void Init();

    // Start is called before the first frame update
    protected new void Awake()
    {
        base.Awake();
        //(아군이면)플레이어 델리게이트 추가
    }

    // Update is called once per frame
    //protected new void Update()
    //{
    //    base.Update();
    //}

    private void OnDestroy()
    {
        //(아군이면)플레이어 델리게이트 해제
    }
}