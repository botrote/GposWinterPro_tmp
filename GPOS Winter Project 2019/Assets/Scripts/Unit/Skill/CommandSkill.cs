using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSkill : ISkill
{
    private const int heal = 20;
    public const int Ccost = 0;
    public const int CunlockCost = 40;

    public override void getCost(out int decknotch, out int unlockCost)
    {
        decknotch = Ccost;
        unlockCost = CunlockCost;
    }

    public override void UseSkill()
    {
        Unit Player = GameObject.Find("Player").GetComponent<Unit>();
        Vector2 cursor = GameObject.Find("Manager").GetComponent<InputManager>().getMousePosition();
        Collider2D[] targets = Physics2D.OverlapCircleAll(cursor, 1.0f);
        foreach (Collider2D target in targets)
        {
            if(target.gameObject.GetComponent<Unit>()!= null && target.gameObject.GetComponent<Unit>().TeamTag == Unit.Team.Friendly && target.gameObject.GetComponent<Player>() == null)
            {
                Vector3 dest = Player.position + new Vector2(Random.Range(1f,2f), Random.Range(1f,2f));
                target.gameObject.transform.position = dest;
                target.gameObject.GetComponent<Unit>().Dest = target.gameObject.transform.position;
                Unit.GiveStun(target.gameObject.GetComponent<Unit>(), 0.5f);
                if(target.gameObject.name != ("Flag(Clone)"))
                    GameObject.Find("Manager").GetComponent<EffectManager>().Delegate_CommandUnit(target.gameObject);
                
            }
        }
        GameObject.Find("Manager").GetComponent<EffectManager>().Delegate_CommandCircle(cursor);
        GameObject.Find("Manager").GetComponent<EffectManager>().Delegate_CommandCircle(Player.position + new Vector2(1.5f, 1.5f));
    }

    public override void UseSkill(Unit user, Unit Target)
    {
    }

    public override void UseSkill(Unit user, Vector2 pos)
    {
    }
}

