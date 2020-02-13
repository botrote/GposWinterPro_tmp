using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적군 원거리 공격 AI, 인식 거리에 적이 있을 경우 적을 추적하여 공격
/// </summary>
public class EnemyMissileAI : AI
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
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        while (true)
        {
            if (Target == null)
            {
                curAction = Action.Idle; 
            }
            else
            {
                if (Vector2.Distance(Target.position, body.position)> ((IMissileAttack)body).getMissileRange()) curAction = Action.Pursue;
                else curAction = Action.Engage;
            }
            
            switch (curAction)
            {
                default:
                case Action.Idle:
                    Target = FindTarget("Friendly");
                    yield return new WaitForSeconds(0.1f);
                    break;
                case Action.Pursue:
                    Target = FindTarget("Friendly");
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