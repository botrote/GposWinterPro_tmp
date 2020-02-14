using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
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
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Friendly"))
        {
            if (!collision.gameObject.tag.Equals(team.ToString()))
            {
                if (collision.gameObject.GetComponent<Unit>() is Player) collision.gameObject.GetComponent<Unit>().Damage((int)damage*2);
                else collision.gameObject.GetComponent<Unit>().Damage((int)damage);
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
