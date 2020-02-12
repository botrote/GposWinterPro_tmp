using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어, 적/아군 유닛의 기반 클래스. 기초 스탯 관리 및 데미지, 이동 관련 기능 포함함.
/// </summary>
public abstract class Unit : MonoBehaviour
{
    /// <summary>
    /// 종족값, 사제의 언데드 추가 데미지 등에 필요함.
    /// </summary>
    public enum Race { None, Undead, Villager, Soldier, Elite, Commander, Hero }
    /// <summary>
    /// 현재 이동 상태(정지 혹은 이동중)
    /// </summary>
    public enum Behaviour { Idle, Moving }
    /// <summary>
    /// 피아 식별용(AI는 게임오브젝트의 태그로 인식할 것을 권장함)
    /// </summary>
    public enum Team { Building, Enemy, Friendly }
    public Behaviour curBehaviour { get; protected set; }
    private Vector2 destpos;
    /// <summary>
    /// 유닛의 이동 목적지
    /// </summary>
    public virtual Vector2 Dest
    {
        get { return destpos; }
        set
        {
            //if(Mathf.Abs(pos.x) > map.x || Mathf.Abs(pos.y) > map.y)
            //Debug.Log(gameObject.ToString() + "moving to" + value.ToString());
            destpos = value;
            curBehaviour = Behaviour.Moving;
        }
    }
    /// <summary>
    /// 최대 체력
    /// </summary>
    public abstract int MaxHealth { get; }
    /// <summary>
    /// 현재 체력
    /// </summary>
    public int curHealth { get; protected set; }
    /// <summary>
    /// 방어도
    /// </summary>
    public abstract int defense { get; }
    /// <summary>
    /// 속도
    /// </summary>
    public abstract float speed { get; }
    /// <summary>
    /// 종족값
    /// </summary>
    public abstract Race race { get; }
    private Rigidbody2D unitRigidbody2D;
    private Transform unitTransform;
    private Coroutine damagedCoroutine;
    private Coroutine HPBuffCoroutine;
    Color original;
    /// <summary>
    /// 현재 유닛의 위치
    /// </summary>
    public Vector2 position { get { return unitTransform.position; } }
    /// <summary>
    /// 유닛의 피아
    /// </summary>
    public abstract Team TeamTag { get; }
    /// <summary>
    /// 유닛의 이름, 팩토리의 product와 일치시켜야 함.
    /// </summary>
    /// <returns></returns>
    public abstract string Unitname { get; }

    public List<IBuff> Buffs;
    
    public bool isStunned
    {
        get
        {
            foreach (IBuff buff in Buffs)
            {
                if (buff.isStun()) return true;
            }
            return false;
        }
    }

    protected void Awake()
    {
        original = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.tag = TeamTag.ToString();
        unitRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        unitTransform = gameObject.GetComponent<Transform>();
        destpos = unitTransform.position;
        curBehaviour = Behaviour.Idle;
        curHealth = MaxHealth;
        Buffs = new List<IBuff>();
        HPBuffCoroutine = StartCoroutine(DOT());
    }
    // Start is called before the first frame update
    protected void Start()
    {

    }
    // Update is called once per frame
    protected void Update()
    {
        foreach(IBuff buff in Buffs)
        {
            if (!buff.Update(Time.deltaTime))
            {
                Buffs.Remove(buff);
            }
        }
        switch (curBehaviour)
        {
            default:
            case Behaviour.Idle:
                unitRigidbody2D.velocity = Vector2.zero;
                break;
            case Behaviour.Moving:
                {
                    if (isStunned) break;
                    Vector2 curpos = unitTransform.position;
                    if (Vector2.Distance(curpos, destpos) > 1.0f)
                    {
                        float spdFactor = 1;
                        foreach(IBuff buff in Buffs)
                        {
                            spdFactor *= buff.getSpdBuff();
                        }
                        unitRigidbody2D.velocity = (destpos - curpos).normalized * speed * spdFactor;
                    }
                    else
                    {
                        unitRigidbody2D.velocity = Vector2.zero;
                        curBehaviour = Behaviour.Idle;
                    }
                }
                break;
        }
    }
    /// <summary>
    /// 유닛이 데미지를 입음, 결과적으로 사망하면 Die() 호출
    /// </summary>
    /// <param name="damage">데미지 값</param>
    public virtual void Damage(int damage)
    {
        float defFactor = 1;
        foreach(IBuff buff in Buffs)
        {
            defFactor *= buff.getDefBuff();
        }
        Debug.Log(gameObject.ToString() + "damaged, dmg : " + damage);
        if (damagedCoroutine != null)
            StopCoroutine(damagedCoroutine);
        damagedCoroutine = StartCoroutine(paintRed());
        if (curHealth - damage * (1f - ((float)(defense)*defFactor)/100) <= 0)
        {
            curHealth = 0;
            Die();
            return;
        }
        else
        {
            curHealth -= (int)(damage * (1f - ((float)(defense) * defFactor) / 100));
        }
    }
    /// <summary>
    /// 유닛의 체력 증가, 최대 체력을 넘지 않도록 조절
    /// </summary>
    /// <param name="damage"></param>
    public void Heal(int amount)
    {
        Debug.Log(this + "Healed!");
        if(MaxHealth <= curHealth + amount)
        {
            curHealth = MaxHealth;
        }
        else
        {
            curHealth += amount;
        }
    }
    /// <summary>
    /// 유닛이 죽었을 때 발생하는 이벤트 등을 처리함(CampaignManager에 플래그를 세우는 등)
    /// </summary>
    protected void Die()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// 유닛을 0.5초간 시뻘겋게 칠함. 데미지 입은 것 표시용.
    /// </summary>
    /// <returns></returns>
    private IEnumerator paintRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,0,0) ;
        yield return new  WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = original;
    }
    private IEnumerator DOT()
    {
        int hpchange = 0;
        foreach (IBuff buff in Buffs)
        {
            hpchange += buff.getHPBuff();
        }
        if (hpchange > 0)
        {
            Heal((int)hpchange);
        }
        else if(hpchange < 0)
        {
            hpchange = 0 - hpchange;
            Damage((int)hpchange);
        }
        yield return new WaitForSeconds(1f);
    }
}
