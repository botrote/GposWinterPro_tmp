using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적군 저격 AI
/// </summary>
public class EnemySniperAI : AI
{
    public enum Action { Pursue, Snipe }
    protected Action curAction;
    protected Player player;
    protected IEnumerator FSMCoroutine;
    
    private void Awake()
    {
        body = gameObject.GetComponent<Unit>();
        player = GameObject.Find("Player").GetComponent<Player>();
        Target = null;
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
            if (Vector2.Distance(player.position, body.position)> ((IMissileAttack)body).getMissileRange()) curAction = Action.Pursue;
            else curAction = Action.Snipe;
            
            switch (curAction)
            {
                default:
                case Action.Pursue:
                    Target = FindTarget("Friendly", 5f);
                    if(Target==null) body.Dest = player.position;
                    else
                    {
                        body.Dest = body.position;
                        ((IMissileAttack)body).Shoot(Target);
                    }
                    yield return null;
                    break;
                case Action.Snipe:
                    body.Dest = body.position;
                    ((IMissileAttack)body).Shoot(player);
                    yield return null;
                    break;
            }
        }
    }
}