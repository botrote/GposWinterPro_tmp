using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race {None, Undead, Villager, Soldier, Elite, Commander, Hero}

public abstract class Unit : MonoBehaviour
{
    protected ushort maxHealth;
    protected ushort curHealth;
    protected ushort defense;
    protected ushort attack;
    protected float speed;
    protected float range;
    protected Race race; //유닛의 종족값(언데드 등)
    
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Damage(ushort damage, Unit attacker) //유닛이 데미지를 입음, 결과적으로 사망하면 Die() 호출
    {
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
    void Move(Vector2 pos) //특정 위치를 향해 이동
    {

    }
    void Die() //유닛이 죽었을 때 발생하는 이벤트 등을 처리함(CampaignManager에 플래그를 세우는 등)
    {

    }
    public abstract int getNumofUnit() //해당 유닛의 개수 반환 
}
