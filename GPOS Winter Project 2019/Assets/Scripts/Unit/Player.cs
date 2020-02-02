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
    public override ushort MaxHealth
    {
        get { return (ushort)(100 + Manager.GetComponent<WaveManager>().getWave * 300); }
    }
    public override ushort defense
    {
        get { return (ushort)(1 + Manager.GetComponent<WaveManager>().getWave * 8); }
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

    // Start is called before the first frame update
    protected void Awake()
    {
        Manager = GameObject.Find("Manager");
        Manager.GetComponent<InputManager>().RightClickInput += new InputManager.CoordInputEventHandler(Move);
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
}
