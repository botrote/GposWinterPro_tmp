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
        Debug.Log("RRRRRRRRRRRRRRRRRRR");
        GameObject [] targets =  GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(targets.Length);
        foreach(GameObject target in targets)
        {
            if(Vector2.Distance(target.transform.position, Target.position)<Radius)
            {
                target.GetComponent<Unit>().Damage(damage);
            }
        }
    }

    public void UseSkill(Vector2 pos)
    {
    }
}

