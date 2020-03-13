using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적군 돌격 공격 AI
/// </summary>
public class EnemyChargeAI : AI
{
    
    public enum Action { Idle, Pursue, LockOn, Charge, Engage}
    protected Action curAction;
    protected Player player;
    protected IEnumerator FSMCoroutine;
    private LineRenderer line;

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
        //Debug.Log(body.ToString() + "has consciousness");
        yield return null;
        while (true)
        {
            if (Target == null)
            {
                ((HorseManL)body).charge=false;
                curAction = Action.Idle;
            }
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
                            curAction = Action.LockOn;
                        }
                        else
                        {
                            body.Dest = player.position;
                        }
                    }
                    yield return null;
                    break;
                case Action.Pursue:
                    Target = FindTarget("Friendly");
                    if(Target!=null) body.Dest = Target.position;
                    yield return null;
                    break;
                case Action.LockOn:
                    for(float time=0; time<3&&Target!=null&&((HorseManL)body).charge; time+=Time.deltaTime)
                    {
                        Drawline(Target);
                        yield return null;
                        Destroy(line);
                    }
                    if(Target!=null&&((HorseManL)body).charge) curAction=Action.Charge;
                    break;
                case Action.Charge:
                    if(Target!=null) body.Dest = Target.position;
                    else curAction=Action.Idle;
                    yield return null;
                    break;
                case Action.Engage:
                    body.Dest=body.position;
                    ((IMeleeAttack)body).MeleeAttack(Target);
                    if(((HorseManL)body).charge)
                    {
                        ((HorseManL)body).charge=false;
                        for(float timer=0f; timer <3f; timer += Time.deltaTime)
                        {
                            Target = FindTarget("Friendly");
                            if(Target!=null)
                            {
                                if (Vector2.Distance(Target.position, body.position) <= ((IMeleeAttack)body).getMeleeRange())
                                {
                                    body.Dest=body.position;
                                    ((IMeleeAttack)body).MeleeAttack(Target);
                                } 
                                else body.Dest = Target.position;
                            } 
                            yield return null;
                        }
                        body.Dest = body.position - (player.position - body.position).normalized*body.speed*5;
                        for(float timer=0f; timer <5f; timer += Time.deltaTime)
                        {   
                            yield return null;
                        }
                        Target = null;
                    }
                    curAction=Action.Pursue;
                    yield return null;
                    break;
            }
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (((HorseManL)body).charge&&collision.gameObject.tag.Equals("Friendly")) Target=collision.gameObject.GetComponent<Unit>();
    }
    private void OnDestroy()
    {
        if(line!=null) Destroy(line);
    }
    private void Drawline(Unit Target)
    {
        if(Target==null) return;
        line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetPosition(0, (Vector3)body.position+new Vector3(0,0,3));
        line.SetPosition(1, (Vector3)Target.position+new Vector3(0,0,3));
        line.startWidth=0.03f;
        line.endWidth=0.03f;
        line.startColor=new Color(1, 0.5f, 0);
        line.endColor=new Color(1, 0.5f, 0);
    }
}
