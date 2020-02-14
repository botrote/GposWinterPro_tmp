using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : MonoBehaviour
{
    Unit.Team team;
    int damage;
    float duration;
    float DamageRadius;
    Flag origin;

    public void Init(Unit.Team _team, int _damage, float _duration, float _DamageRadius, Flag _Flag)
    {
        team = _team;
        damage = _damage;
        duration = _duration;
        DamageRadius = _DamageRadius;
        origin = _Flag;
        StartCoroutine(Effect());
    }
    protected IEnumerator Effect()
    {
        yield return new WaitForFixedUpdate();
        while(origin!=null)
        {
            Collider2D[] Targets = Physics2D.OverlapCircleAll(gameObject.transform.position, DamageRadius);
            Debug.Log("Targets length: " + Targets.Length);
            for(int i=0; i<Targets.Length; i++)
            {
                if(Targets[i].gameObject.GetComponent<Unit>()==null) continue;
                else
                {
                    Debug.Log(i.ToString() + " : " + Targets[i].gameObject.ToString());
                    if ( Targets[i].gameObject.GetComponent<Unit>().tag.Equals("Enemy") || Targets[i].gameObject.GetComponent<Unit>().tag.Equals("Friendly"))
                    {
                        if(Targets[i].gameObject.GetComponent<Unit>().TeamTag != team) Targets[i].gameObject.GetComponent<Unit>().Buffs.Add(new Terror());
                    }           
                }        
            }
            yield return null;
        }
        Destroy(gameObject);
    }
    
    protected void OnDestroy()
    {
    }
}
