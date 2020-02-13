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
        Debug.Log(body.ToString() + "has consciousness");
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        while (true)
        {
            if (Target == null) curAction = Action.Idle;
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
                    }
                    yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Pursue:
                    Target = FindTarget("Friendly");
                    body.Dest = Target.position;
                    yield return new WaitForSeconds(0.5f);
                    break;
                case Action.Charge:
                    body.Dest = Target.position;
                    yield return new WaitForSeconds(0.5f);
                    break;
                case Action.Engage:
                    ((IMeleeAttack)body).MeleeAttack(Target);
                    if(((HorseManL)body).charge)
                    {
                        ((HorseManL)body).charge=false;
                        for(int i=0; i<25; i++)
                        {
                            Target = FindTarget("Friendly");
                            if (Vector2.Distance(Target.position, body.position) <= ((IMeleeAttack)body).getMeleeRange()) ((IMeleeAttack)body).MeleeAttack(Target);
                            else body.Dest = Target.position;
                            yield return new WaitForSeconds(0.1f);
                        }
                        Target = FindTarget("Friendly");
                        if(Target!=null)
                        {
                            for(int i=0; i<50; i++)
                            {
                                body.Dest = Target.position - (body.position - Target.position).normalized*(0.1f*body.speed);
                                yield return new WaitForSeconds(0.1f);
                            }
                            Target = FindTarget("Friendly");
                        }
                    }
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
    }
}
