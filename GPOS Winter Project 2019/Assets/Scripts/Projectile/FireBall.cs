using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Unit.Team team;
    int damage;
    float duration;
    float DamageRadius;

    public void Init(Unit.Team _team, int _damage, float _duration, float _DamageRadius)
    {
        team = _team;
        damage = _damage;
        duration = _duration;
        DamageRadius = _DamageRadius;
        StartCoroutine(LifeTime(duration));
    }
    
    

    protected IEnumerator LifeTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        Collider2D[] Targets = Physics2D.OverlapCircleAll(this.gameObject.GetComponent<Transform>().position, DamageRadius);
        for (int i = 0; i < Targets.Length; i++)
        {
            if (Targets[i].gameObject.GetComponent<Unit>() == null) continue;
            else
            {
                if (Targets[i].gameObject.GetComponent<Unit>().TeamTag != team) Targets[i].gameObject.GetComponent<Unit>().Damage((uint)damage);
            }
        }
    }
}
