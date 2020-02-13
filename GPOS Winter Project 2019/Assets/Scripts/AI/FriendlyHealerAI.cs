using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아군 힐러 AI, 인식 거리내의 체력 비율이 가장 낮은 아군을 쫒아가 치유, 없을 경우 마왕 주위로 돌아옴.
/// </summary>
public class FriendlyHealerAI : AI
{
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
        yield return null;
        while (true)
        {
            if (Target == null)
            {
                if (Vector2.Distance(player.position, body.position) > (MaxDist-2.0f)) curAction = Action.Rally;
                else curAction = Action.Idle; 
            }
            else
            {
                if (Vector2.Distance(Target.position, body.position) > ((IHealer)body).getHealRange()) curAction = Action.Pursue;
                else curAction = Action.Heal;
            }
            //if (player.curBehaviour == Unit.Behaviour.Moving) body.Dest = body.position + (player.Dest - player.position);
            switch (curAction)
            {
                default:
                case Action.Idle:
                    Target = FindTarget();
                    yield return null;
                    break;
                case Action.Rally:
                    body.Dest = player.position + (body.position - player.position).normalized * (MaxDist - 3.0f);
                    Target = FindTarget();
                    yield return null;
                    break;
                case Action.Pursue:
                    body.Dest = Target.position + (body.position - Target.position).normalized * ((IHealer)body).getHealRange();
                    yield return null;
                    break;
                case Action.Heal:
                    ((IHealer)body).Heal(Target);
                    yield return null;
                    break;
            }
        }
    }
    protected Unit FindTarget()
    {
        Collider2D[] Targets = Physics2D.OverlapCircleAll(body.position, 7.0f);
        Unit CurTarget=null;
        for(int i=0; i<Targets.Length; i++)
        {
            if(Targets[i]==null) break;
            else if(Targets[i].tag.Equals("Enemy")) continue;
            else if(CurTarget==null||CurTarget==body) CurTarget=Targets[i].gameObject.GetComponent<Unit>();
            else if((float)CurTarget.curHealth/CurTarget.MaxHealth > (float)(Targets[i].gameObject.GetComponent<Unit>().curHealth)/Targets[i].gameObject.GetComponent<Unit>().MaxHealth)
            {
                CurTarget=Targets[i].gameObject.GetComponent<Unit>();
            }    
        }
        if(CurTarget==body||CurTarget.curHealth==CurTarget.MaxHealth) return null;
        return CurTarget;
    }
}