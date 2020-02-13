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
    public Deck(ISkill _skill)
    {
        isUnit = false;
        skill = _skill;
    }
    public void useDeck(Vector2 pos)
    {
        if (isUnit)
        {
            GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>().PlaceUnit(unit, pos);
        }
        else
        {

        }
    }
    public int getcost()
    {
        return cost;
    }
}
