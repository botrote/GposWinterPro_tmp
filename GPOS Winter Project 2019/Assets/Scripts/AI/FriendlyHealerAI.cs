using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아군 힐러 AI, 인식 거리내의 체력 비율이 가장 낮은 아군을 쫒아가 치유, 없을 경우 마왕 주위로 돌아옴.
/// </summary>
public class FriendlyHealerAI : AI
{
    protected Unit Target;
    public enum Action { Idle, Rally, Pursue, Heal }
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
                    if (Vector2.Distance(Target.position, body.position) < ((IHealer)body).getHealRange()) curAction = Action.Heal;
                    else
                    {
                        body.Dest = Target.position;
                        yield return new WaitForSeconds(0.1f);
                    }
                    break;
                case Action.Heal:
                    if (Vector2.Distance(Target.position, body.position) >= ((IHealer)body).getHealRange()) curAction = Action.Pursue;
                    else ((IHealer)body).Heal(Target);
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
    }
    protected Unit FindTarget()
    {
        GameObject[] possibletargets = GameObject.FindGameObjectsWithTag("Friendly");
        if (possibletargets.Length == 0) return null;
        Unit curTarget = possibletargets[0].GetComponent<Unit>();
        float hpCurTarget = (float)curTarget.curHealth/curTarget.MaxHealth;
        if (curTarget == body) curTarget = possibletargets[1].GetComponent<Unit>();
        for (int i = 1; i < possibletargets.Length; i++)
        {
            if (possibletargets[i].GetComponent<Unit>() != body)
            {
                if (Vector2.Distance(possibletargets[i].GetComponent<Unit>().position, player.position) < MaxBattleDist
                && hpCurTarget > (float)(possibletargets[i].GetComponent<Unit>().curHealth)/(possibletargets[i].GetComponent<Unit>().MaxHealth))
                {
                    curTarget = possibletargets[i].GetComponent<Unit>();
                    hpCurTarget = (float)(possibletargets[i].GetComponent<Unit>().curHealth)/(possibletargets[i].GetComponent<Unit>().MaxHealth);
                }
            }
        }
        if (Vector2.Distance(curTarget.position, player.position) >= MaxBattleDist) return null;
        return curTarget;
    }
}