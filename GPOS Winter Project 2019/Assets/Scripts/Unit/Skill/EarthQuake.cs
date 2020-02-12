using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : ISkill
{
    private const float Radius = 10f;
    private const int damage = 20;

    public void UseSkill()
    {
        Unit Player = GameObject.Find("Player").GetComponent<Unit>();
        Collider2D[] subjects = Physics2D.OverlapCircleAll(Player.position, Radius);
        Debug.Log(subjects.Length);
        foreach (Collider2D subject in subjects)
        {
            if (subject.gameObject.tag.Equals("Enemy") || subject.gameObject.tag.Equals("Friendly"))
            {
                if (!subject.gameObject.tag.Equals(Player.TeamTag.ToString()))
                {
                    subject.gameObject.GetComponent<Unit>().Damage(damage);
                }
            }
        }
    }

    public void UseSkill(Unit user, Unit Target)
    {
        Collider2D[] subjects = Physics2D.OverlapCircleAll(user.position, Radius);
        Debug.Log(subjects.Length);
        foreach (Collider2D subject in subjects)
        {
            if (subject.gameObject.tag.Equals("Enemy")||subject.gameObject.tag.Equals("Friendly"))
            {
                if (!subject.gameObject.tag.Equals(user.TeamTag.ToString()))
                {
                    subject.gameObject.GetComponent<Unit>().Damage(damage);
                }
            }
        }
    }

    public void UseSkill(Unit user, Vector2 pos)
    {
    }
}

