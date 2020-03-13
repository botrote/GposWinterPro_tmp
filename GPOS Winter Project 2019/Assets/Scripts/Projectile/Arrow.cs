using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Unit.Team team;
    int damage;
    float duration;

    public void Init(Unit.Team _team, int _damage, float _duration)
    {
        team = _team;
        damage = _damage;
        duration = _duration;
        StartCoroutine(LifeTime(duration));
        StartCoroutine(BuildEffect());
    }

    IEnumerator BuildEffect()
    {
        yield return new WaitForEndOfFrame();
        EffectManager effectManager = GameObject.Find("Manager").GetComponent<EffectManager>();
        if(gameObject.name.Equals("Death(Clone)"))
            StartCoroutine(effectManager.BuildDeathProjectile(gameObject));
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Friendly"))
        {
            if (!collision.gameObject.tag.Equals(team.ToString()))
            {
                EffectManager effectManager = GameObject.Find("Manager").GetComponent<EffectManager>();
                if(gameObject.name.Equals("Death(Clone)"))
                    StartCoroutine(effectManager.BuildDeathExplosion(gameObject.transform.position));
                collision.gameObject.GetComponent<Unit>().Damage(damage);
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
