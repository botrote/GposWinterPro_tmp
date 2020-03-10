using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적군 돌격 공격 AI
/// </summary>
public class EnemyChargeAI : AI
{
    
    public enum Action { Idle, Pursue, Charge, Engage}
    protected Action curAction;
    protected Player player;
    protected IEnumerator FSMCoroutine;

    private void Awake()
    {
        body = gameObject.GetComponent<Unit>();
        Target = null;
        ((HorseManL)body).charge=false;
        player = GameObject.Find("Player").GetComponent<Player>();
        FSMCoroutine = FSM();
        StartCoroutine(FSMCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public IEnumerator FSM()
    {
        //Debug.Log(body.ToString() + "has consciousness");
        yield return null;
        while (true)
        {
            if (Target == null)
            {
                ((HorseManL)body).charge=false;
                curAction = Action.Idle;
            }
            else if (Vector2.Distance(Target.position, body.position) <= ((IMeleeAttack)body).getMeleeRange()) curAction=Action.Engage;
            switch (curAction)
            {
                default:
                case Action.Idle:
                    Target = FindTarget("Friendly");
                    if(Target!=null) curAction=Action.Pursue;
                    else
                    {
                        Target = FindTarget("Friendly", 40f);
                        if(Target!=null)
                        {
                            ((HorseManL)body).charge=true;
                            curAction = Action.Charge;
                        }
                        else
                        {
                            body.Dest = player.position;
                        }
                    }
                    yield return null;
                    break;
                case Action.Pursue:
                    Target = FindTarget("Friendly");
                    if(Target!=null) body.Dest = Target.position;
                    yield return null;
                    break;
                case Action.Charge:
                    if(Target!=null) body.Dest = Target.position;
                    yield return null;
                    break;
                case Action.Engage:
                    ((IMeleeAttack)body).MeleeAttack(Target);
                    if(((HorseManL)body).charge)
                    {
                        ((HorseManL)body).charge=false;
                        for(float timer=0f; timer <3f; timer += Time.deltaTime)
                        {
                            Target = FindTarget("Friendly");
                            if(Target!=null)
                            {
                                if (Vector2.Distance(Target.position, body.position) <= ((IMeleeAttack)body).getMeleeRange())
                                {
                                    body.Dest=body.position;
                                    ((IMeleeAttack)body).MeleeAttack(Target);
                                } 
                                else body.Dest = Target.position;
                            } 
                            yield return null;
                        }
                        body.Dest = body.position - (player.position - body.position).normalized*body.speed*5;
                        for(float timer=0f; timer <5f; timer += Time.deltaTime)
                        {   
                            yield return null;
                        }
                        Target = null;
                    }
                    yield return null;
                    break;
            }
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Friendly")) Target=collision.gameObject.GetComponent<Unit>();
    }
}
