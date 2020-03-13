using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
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

    private IEnumerator BuildEffect()
    {
        Debug.Log("called1");
        yield return new WaitForEndOfFrame();
        EffectManager effectManager = GameObject.Find("Manager").GetComponent<EffectManager>();

        if(gameObject.name.Equals("Holy(Clone)"))
        {
            Debug.Log("called2");
            StartCoroutine(effectManager.BuildHolyProjectile(gameObject));
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Friendly"))
        {
            if (!collision.gameObject.tag.Equals(team.ToString()))
            {
                EffectManager effectManager = GameObject.Find("Manager").GetComponent<EffectManager>();
                collision.gameObject.GetComponent<Unit>().Damage((int)damage);
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
