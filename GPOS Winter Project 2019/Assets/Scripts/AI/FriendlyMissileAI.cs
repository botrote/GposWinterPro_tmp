using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아군 원거리 공격 AI, 인식 거리에 적이 있을 경우 적을 추적하여 공격, 없을 경우 마왕 주위로 돌아옴.
/// </summary>
public class FriendlyMissileAI : AI
{
    protected Unit Target;
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
                if (Vector2.Distance(player.position, body.position) >= MaxDist) curAction = Action.Rally;
                else curAction = Action.Idle; 
            }
            if (player.curBehaviour == Unit.Behaviour.Moving) body.Dest = body.position + (player.Dest - player.position);
            switch (curAction)
            {
                default:
                case Action.Idle:
                    Target = FindTarget();
                    if (Target != null) curAction = Action.Pursue;
                    else yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Rally:
                    Target = FindTarget();
                    if (Target != null) curAction = Action.Pursue;
                    else body.Dest = player.position + (body.position - player.position).normalized * (MaxDist - 1.0f);
                    yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Pursue:
                    if (Vector2.Distance(Target.position, body.position) < ((IMissileAttack)body).getMissileRange()) curAction = Action.Engage;
                    else
                    {
                        body.Dest = Target.position;
                        yield return new WaitForSeconds(0.1f);
                    }
                    break;
                case Action.Engage:
                    if (Vector2.Distance(Target.position, body.position) >= ((IMissileAttack)body).getMissileRange()) curAction = Action.Pursue;
                    else ((IMissileAttack)body).Shoot(Target);
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
    }
    protected Unit FindTarget()
    {
        GameObject[] possibletargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (possibletargets.Length == 0) return null;
        Unit curTarget = possibletargets[0].GetComponent<Unit>();
        float distanceCurTarget = Vector2.Distance(possibletargets[0].GetComponent<Unit>().position, body.position);
        for (int i = 1; i < possibletargets.Length; i++)
        {
            if (Vector2.Distance(possibletargets[i].GetComponent<Unit>().position, player.position) < MaxBattleDist
                && distanceCurTarget > Vector2.Distance(possibletargets[i].GetComponent<Unit>().position, body.position))
            {
                curTarget = possibletargets[i].GetComponent<Unit>();
                distanceCurTarget = Vector2.Distance(curTarget.position, body.position);
            }
        }
        if (Vector2.Distance(curTarget.position, player.position) >= MaxBattleDist) return null;
        return curTarget;
    }
}

