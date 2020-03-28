using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapDot : MonoBehaviour
{
    public RectTransform rect;
    public GameObject Boundary;
    public Unit unitPointing;
    // Start is called before the first frame update
    void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
        Boundary = GameObject.Find("MapManager").transform.Find("Boundary").gameObject;
    }

    public void setUnit(Unit _unit)
    {
        unitPointing = _unit;
        Color pointColor = new Color();
        switch (unitPointing.TeamTag) {
            default:
            case Unit.Team.Enemy:
                pointColor = Color.red;
                break;
            case Unit.Team.Building:
                pointColor = Color.white;
                break;
            case Unit.Team.Friendly:
                pointColor = Color.blue;
                break;
        }
        gameObject.GetComponent<Image>().color = pointColor;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (unitPointing != null)
        {
            Vector2 UnitPos = unitPointing.transform.position - Boundary.transform.position;
            rect.localPosition = UnitPos * 5 / 2;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
