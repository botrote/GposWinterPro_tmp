using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아군 원거리 공격 AI, 인식 거리에 적이 있을 경우 적을 추적하여 공격, 없을 경우 마왕 주위로 돌아옴.
/// </summary>
public class FriendlyMissileAI : AI
{
    public enum Action { Idle, Rally, Pursue, Engage }
    protected Action curAction;
    protected Player player;
    protected IEnumerator FSMCoroutine;
    
    private void Awake()
    {
        body = gameObject.GetComponent<Unit>();
        Target = null;
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
            if (Target == null)
            {
                if (Vector2.Distance(player.position, body.position) > (MaxDist-1.0f)) curAction = Action.Rally;
                else curAction = Action.Idle; 
            }
            else
            {
                if (Vector2.Distance(Target.position, body.position) > ((IMissileAttack)body).getMissileRange()) curAction = Action.Pursue;
                else curAction = Action.Engage;
            }
            //if (player.curBehaviour == Unit.Behaviour.Moving) body.Dest = body.position + (player.Dest - player.position);
            switch (curAction)
            {
                default:
                case Action.Idle:
                    Target = FindTarget("Enemy");
                    yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Rally:
                    body.Dest = player.position + (body.position - player.position).normalized * (MaxDist - 2.0f);
                    Target = FindTarget("Enemy");
                    yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Pursue:
                    body.Dest = Target.position + (body.position - Target.position).normalized * ((IMissileAttack)body).getMissileRange()*0.8f;
                    yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Engage:
                    ((IMissileAttack)body).Shoot(Target);
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
    }
}

