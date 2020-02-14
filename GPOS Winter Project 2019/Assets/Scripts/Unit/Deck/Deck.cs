using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public bool isUnit
    {
        get;
        protected set;
    }
    private string unit;
    private ISkill skill;
    private int cost;

    public Deck(string _unit, int _cost)
    {
        isUnit = true;
        unit = _unit;
        cost = _cost;
    }
    public Deck(ISkill _skill, int _cost)
    {
        isUnit = false;
        skill = _skill;
        cost = _cost;
    }
    public void useDeck(Vector2 pos)
    {
        if (isUnit)
        {
            GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>().PlaceUnit(unit, pos);
        }
        else
        {
            skill.UseSkill();
        }
    }
    public int getcost()
    {
        return cost;
    }
}
