using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아군 근접 공격 AI, 현재 구현된 기능은 플레이어와 일정 거리 이하를 유지하는 것 외에는 없음.
/// </summary>
public class FriendlyMeleeAI : AI
{
    protected Unit Target;
    public const float MaxDist = 3f;
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
            if (Vector2.Distance(player.position, body.position) >= MaxDist) curAction = Action.Rally;
            switch (curAction)
            {
                default:
                case Action.Idle:
                    if (Target != null) curAction = Action.Pursue;
                    else yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Rally:
                    Debug.Log("Rallying");
                    if (Vector2.Distance(player.position, body.position) < MaxDist) curAction = Action.Idle;
                    body.Dest = player.position + (body.position - player.position).normalized * MaxDist;
                    yield return new WaitForSeconds(0.5f);
                    break;
                case Action.Pursue:
                    if (Vector2.Distance(Target.position, body.position) < ((IMeleeAttack)body).getMeleeRange()) curAction = Action.Engage;
                    else
                    {
                        body.Dest = body.position + (Target.position - body.position).normalized;
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case Action.Engage:
                    ((IMeleeAttack)body).MeleeAttack(Target);
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
    }
}
