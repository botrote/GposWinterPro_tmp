using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 제외하고 아군, 적 모든 유닛의 기반 클래스.
/// </summary>
public abstract class NPC : Unit
{

    public static float friendlyHealthFactor
    {
        get { return 1; }
    }
    public static float friendlyAttackFactor
    {
        get { return 1; }
    }
    public static float friendlySpeedFactor
    {
        get { return 1; }
    }
    public static float friendlyDefenseFactor
    {
        get { return 1; }
    }
    public override int MaxHealth
    {
        get { return (int)((TeamTag == Team.Friendly ? friendlyHealthFactor : 1) * NPCMaxHealth); }
    }
    public override int defense
    {
        get { return (int)((TeamTag == Team.Friendly ? friendlyDefenseFactor : 1) * NPCdefense); }
    }
    public override float speed
    {
        get { return (int)((TeamTag == Team.Friendly ? friendlySpeedFactor : 1) * NPCspeed); }
    }
    public abstract int NPCMaxHealth { get; }
    public abstract int NPCdefense { get; }
    public abstract float NPCspeed { get; }
    protected abstract float RateOfSpecialAttack { get; }
    /// <summary>
    /// 유닛의 놋치(적 유닛에게는 아무 값이나 넣어도 됨)
    /// </summary>
    public abstract int Notch { get; }
    /// <summary>
    /// 유닛을 죽였을 때 얻는 경험치(아군 유닛은 0으로 할 것을 권장함)
    /// </summary>
    public abstract int Exp { get; }
    /// <summary>
    /// 변수 초기화 함수
    /// </summary>
    protected abstract void Init();

    // Start is called before the first frame update
    protected new void Awake()
    {
        base.Awake();
        Init();
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