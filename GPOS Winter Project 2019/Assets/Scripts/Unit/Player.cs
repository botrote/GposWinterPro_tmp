using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private const string unitname = "Player";
    private const float playerSpeed = 5.0f;
    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override uint MaxHealth
    {
        get { return (uint)(100 + Manager.GetComponent<WaveManager>().getWave * 300); }
    }
    public override uint defense
    {
        get { return (uint)(1 + Manager.GetComponent<WaveManager>().getWave * 8); }
    }
    public override float speed
    {
        get { return playerSpeed; }
    }
    public override Race race
    {
        get { return Race.None; }
    }
    public override string Unitname
    {
        get { return unitname; }
    }

    protected GameObject Manager;
    protected ISkill skill;

    // Start is called before the first frame update
    protected void Awake()
    {
        skill = new EarthQuake();
        Manager = GameObject.Find("Manager");
        Manager.GetComponent<InputManager>().RightClickInput += new InputManager.CoordInputEventHandler(Move);
        Manager.GetComponent<InputManager>().PressQ += new InputManager.InputEventHandler(Useskill);
        base.Awake();
    }
    protected void Start()
    {
        base.Start();
        
    }
    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }

    protected void Move(Vector2 pos)
    {
        Dest = pos;
    }
    protected void Useskill()
    {
        skill.UseSkill(this);
    }
}
