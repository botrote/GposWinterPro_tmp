using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아군 근접 공격 AI, 현재 구현된 기능은 플레이어와 일정 거리 이하를 유지하는 것 외에는 없음.
/// </summary>
public class EnemyMeleeAI : AI
{
    protected Unit Target;
    public enum Action { Idle, Pursue, Engage }
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
            switch (curAction)
            {
                default:
                case Action.Idle:
                    Target = FindTarget();
                    if (Target != null) curAction = Action.Pursue;
                    else yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Pursue:
                    if (Target == null) curAction = Action.Idle;
                    if (Vector2.Distance(Target.position, body.position) < ((IMeleeAttack)body).getMeleeRange()) curAction = Action.Engage;
                    else
                    {
                        body.Dest = Target.position - (Target.position - body.position).normalized * (((IMeleeAttack)body).getMeleeRange() - 1.0f);
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case Action.Engage:
                    if (Target == null) curAction = Action.Idle;
                    if (Vector2.Distance(Target.position, body.position) > ((IMeleeAttack)body).getMeleeRange()) curAction = Action.Idle;
                    ((IMeleeAttack)body).MeleeAttack(Target);
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
    }
    protected Unit FindTarget()
    {
        GameObject [] possibletargets = GameObject.FindGameObjectsWithTag("Friendly");
        if(possibletargets.Length == 0) return null;
        Unit curTarget = possibletargets[0].GetComponent<Unit>();
        float distanceCurTarget = Vector2.Distance(possibletargets[0].GetComponent<Unit>().position, body.position); 
        for (int i = 1; i < possibletargets.Length; i++)
        {
            if(distanceCurTarget < Vector2.Distance(possibletargets[i].GetComponent<Unit>().position, body.position))
            {
                curTarget = possibletargets[i].GetComponent<Unit>();
                distanceCurTarget = Vector2.Distance(curTarget.position, body.position);
            }
        }
        return curTarget;
    }
}
