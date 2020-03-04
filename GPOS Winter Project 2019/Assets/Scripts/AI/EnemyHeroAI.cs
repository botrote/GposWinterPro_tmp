using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적군 용사 AI
/// </summary>
public class EnemyHeroAI : AI
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
        StartCoroutine(skill());
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
                    if(Target==null) body.Dest = player.position;
                    yield return null;
                    break;
                case Action.Pursue:
                    Target = FindTarget("Friendly");
                    if(Target!=null) body.Dest = Target.position;
                    yield return null;
                    break;
                case Action.Engage:
                    body.Dest = body.position;
                    ((IMeleeAttack)body).MeleeAttack(Target);
                    yield return null;
                    break;
            }
        }
    }

    public IEnumerator skill()
    {
        yield return null;
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0f,0.5f));
            ((Hero)body).TeleportAttack(Target);
            yield return new WaitForSeconds(Random.Range(0f,0.5f));
            ((Hero)body).FistAttack(Target);
            yield return new WaitForSeconds(Random.Range(0f,0.5f));
            ((Hero)body).BeamAttack(Target);
            yield return new WaitForSeconds(Random.Range(0f,0.5f));
            ((Hero)body).ChargeAttack(player);
        }
    }
}
