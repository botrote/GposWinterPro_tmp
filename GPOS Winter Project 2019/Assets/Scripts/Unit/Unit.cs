using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Unit : MonoBehaviour
{
    public enum Race { None, Undead, Villager, Soldier, Elite, Commander, Hero }
    public enum Behaviour { Idle, Moving }
    public enum Team { Enemy, Friendly }
    private Behaviour curBehaviour;
    private Vector2 destpos;
    /// <summary>
    /// 유닛의 이동 목적지
    /// </summary>
    protected Vector2 Dest
    {
        get { return destpos; }
        set
        {
            //if(Mathf.Abs(pos.x) > map.x || Mathf.Abs(pos.y) > map.y)
            Debug.Log(gameObject.ToString() + "moving to" + value.ToString());
            destpos = value;
            curBehaviour = Behaviour.Moving;
        }
    }
    private Unit pursuit;
    public abstract ushort MaxHealth { get; }
    protected ushort curHealth;
    protected ushort defense;
    protected float speed;
    protected Race race; //유닛의 종족값(언데드 등)
    private Rigidbody2D unitRigidbody2D;
    private Transform unitTransform;
    private Coroutine damagedCoroutine;
    public Vector2 position { get { return unitTransform.position; } }
    public abstract Team TeamTag { get; } 

    protected void Awake()
    {
        gameObject.tag = TeamTag.ToString();
        unitRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        unitTransform = gameObject.GetComponent<Transform>();
        destpos = unitTransform.position;
        curBehaviour = Behaviour.Idle;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    protected void Update()
    {
        switch (curBehaviour)
        {
            default:
            case Behaviour.Idle:
                break;
            case Behaviour.Moving:
                {
                    Vector2 curpos = unitTransform.position;
                    if (Vector2.Distance(curpos, destpos) > 1.0f)
                    {
                        unitRigidbody2D.velocity = (destpos - curpos).normalized * speed;
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
    /// <param name="attacker">공격자 유닛</param>
    public void Damage(ushort damage, Unit attacker)
    {
        Debug.Log( gameObject.ToString() + "damaged, dmg : " + damage);
        if(damagedCoroutine != null)
            StopCoroutine(damagedCoroutine);
        damagedCoroutine = StartCoroutine(paintRed());
        if (curHealth - damage / defense <= 0)
        {
            curHealth = 0;
            Die();
            return;
        }
        else
        {
            curHealth -= (ushort)(damage / defense);
        }
    }
    /// <summary>
    /// 유닛이 데미지를 입음, 결과적으로 사망하면 Die() 호출
    /// </summary>
    /// <param name="damage">데미지 값</param>
    public void Damage(ushort damage)
    {
        Debug.Log(gameObject.ToString() + "damaged, dmg : " + damage);
        if (damagedCoroutine != null)
            StopCoroutine(damagedCoroutine);
        damagedCoroutine = StartCoroutine(paintRed());
        if (curHealth - damage / defense <= 0)
        {
            curHealth = 0;
            Die();
            return;
        }
        else
        {
            curHealth -= (ushort)(damage / defense);
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
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
    }
}
