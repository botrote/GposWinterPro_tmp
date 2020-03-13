using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Friendly"))
        {
            if (!collision.gameObject.tag.Equals(team.ToString()))
            {
                Collider2D[] Targets = Physics2D.OverlapCircleAll(collision.gameObject.GetComponent<Unit>().position, DamageRadius);
                Debug.Log("Targets length: " + Targets.Length);
                for(int i=0; i<Targets.Length; i++)
                {
                    if(Targets[i].gameObject.GetComponent<Unit>()==null) continue;
                    else
                    {
                        Debug.Log(i.ToString() + " : " + Targets[i].gameObject.ToString());
                        if(Targets[i].gameObject.GetComponent<Unit>().TeamTag != team) Targets[i].gameObject.GetComponent<Unit>().Damage(damage);
                    }        
                }
                EffectManager effectManager = GameObject.Find("Manager").GetComponent<EffectManager>();
                if(gameObject.name.Equals("Explosive(Clone)"))
                    StartCoroutine(effectManager.BuildExplosiveEffect(gameObject.transform.position));
                else if(gameObject.name.Equals("Flame(Clone)"))
                    StartCoroutine(effectManager.BuildBurnEffect(gameObject.transform.position));
                Destroy(gameObject);
            }
        }

    }

    protected IEnumerator LifeTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    protected void OnDestroy()
    {
    }
}
