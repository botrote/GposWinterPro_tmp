using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적군 근접 공격 AI
/// </summary>
public class EnemyMeleeAI : AI
{
    
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
        yield return null;
        while (true)
        {
            if (Target == null) curAction = Action.Idle;
            else
            {
                if (Vector2.Distance(Target.position, body.position) >= ((IMeleeAttack)body).getMeleeRange()) curAction = Action.Pursue;
                else curAction=Action.Engage;
            }
            switch (curAction)
            {
                default:
                case Action.Idle:
                    Target = FindTarget("Friendly");
                    yield return null;
                    break;
                case Action.Pursue:
                    Target = FindTarget("Friendly");
                    body.Dest = Target.position;
                    yield return null;
                    break;
                case Action.Engage:
                    ((IMeleeAttack)body).MeleeAttack(Target);
                    yield return null;
                    break;
            }
        }
    }
}
