using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyMeleeAI : AI
{
    public enum Action {Idle, Rally, Pursue, Engage }
    protected Action curAction;
    protected GameObject player;
    public FriendlyMeleeAI(Unit paramUnit) : base(paramUnit)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (curAction)
        {
            default:
            case Action.Idle:
                break;
            case Action.Rally:

                break;
            case Action.Pursue:
                break;
            case Action.Engage:
                break;
        }
    }
}
