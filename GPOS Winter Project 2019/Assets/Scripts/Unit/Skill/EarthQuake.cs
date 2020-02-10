using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : ISkill
{
    private const float Radius = 10f;
    private const uint damage = 20;

    public void UseSkill()
    {
    }

    public void UseSkill(Unit Target)
    {
        Collider2D[] subjects = Physics2D.OverlapCircleAll(Target.position, Radius);
        Debug.Log(subjects.Length);
        foreach (Collider2D subject in subjects)
        {
            if (subject.gameObject.tag.Equals("Enemy")||subject.gameObject.tag.Equals("Friendly"))
            {
                if (!subject.gameObject.tag.Equals(Target.TeamTag.ToString()))
                {
                    subject.gameObject.GetComponent<Unit>().Damage(damage);
                }
            }
        }
    }

    public void UseSkill(Vector2 pos)
    {
    }
}

