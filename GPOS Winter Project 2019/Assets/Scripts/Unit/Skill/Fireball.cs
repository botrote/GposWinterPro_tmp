using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ISkill
{
    private const int damage = 50;
    private const float speed = 5f;

    public void UseSkill()
    {
        Unit Player = GameObject.Find("Player").GetComponent<Unit>();
        Vector2 cursor = GameObject.Find("Manager").GetComponent<InputManager>().getMousePosition();
        float duration = Vector2.Distance(Player.position, cursor) / speed;
        GameObject.Find("ProjectileFactory").GetComponent<ProjectileFactoryManager>().PlaceProjectile("Arrow", Player, Player.position, cursor, damage, speed, duration);
    }

    public void UseSkill(Unit user, Unit Target)
    {
    }

    public void UseSkill(Unit user, Vector2 pos)
    {
    }
}

