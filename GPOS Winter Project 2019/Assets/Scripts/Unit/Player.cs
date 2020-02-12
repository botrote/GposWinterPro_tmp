using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private const string unitname = "Player";
    private const float playerSpeed = 5.0f;
    private int Exp;
    public override Team TeamTag
    {
        get { return Team.Friendly; }
    }
    public override int MaxHealth
    {
        get { return (100 + Manager.GetComponent<WaveManager>().getWave * 300); }
    }
    public override int defense
    {
        get { return (1 + Manager.GetComponent<WaveManager>().getWave * 8); }
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
    protected Deck[] deck;
    protected bool[] deckunlocked;
    protected int chosenDeck;

    // Start is called before the first frame update
    protected void Awake()
    {
        Exp = 40;
        deck = new Deck[9];
        deckunlocked = new bool[9];
        for(int i = 0; i < deck.Length; i++)
        {
            if (i < 2) deckunlocked[i] = true;
            else deckunlocked[i] = false;
        }
        string deckname;
        int decknotch;
        NPC.getNameAndCost<SkeletonB>(out decknotch, out deckname);
        deck[0] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<SkeletonS>(out decknotch, out deckname);
        deck[1] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Orc>(out decknotch, out deckname);
        deck[2] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Ghost>(out decknotch, out deckname);
        deck[3] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Lich>(out decknotch, out deckname);
        deck[4] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Troll>(out decknotch, out deckname);
        deck[5] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Goblin>(out decknotch, out deckname);
        deck[6] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Devil>(out decknotch, out deckname);
        deck[7] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Dragon>(out decknotch, out deckname);
        deck[8] = new Deck(deckname, decknotch);
        chosenDeck = 0;
        Manager = GameObject.Find("Manager");
        Manager.GetComponent<InputManager>().RightClickInput += new InputManager.CoordInputEventHandler(Move);
        Manager.GetComponent<InputManager>().LeftClickInput += new InputManager.CoordInputEventHandler(UseDeck);
        Manager.GetComponent<InputManager>().PressKey += new InputManager.InputEventHandler(UseSkill);
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

    public void addExp(int _exp)
    {
        Exp += _exp;
    }

    public bool subtractExp(int _exp)
    {
        if (Exp < _exp) return false;
        else
        {
            Exp -= _exp;
            return true;
        }
    }

    public int getExp()
    {
        return Exp;
    }

    protected void Move(Vector2 pos)
    {
        Dest = pos;
    }
    protected void UseDeck(Vector2 pos)
    {
        if (subtractExp(deck[chosenDeck].getcost()))
        {
            deck[chosenDeck].useDeck(pos);
        }
        else
        {
            Debug.Log("Unsufficient Exp! : " + Exp);
        }
        
    }

    protected void UseSkill(KeyCode key)
    {
        if(key <= KeyCode.Alpha9 && key >= KeyCode.Alpha1)
        {
            if (deck[key - KeyCode.Alpha1] != null)
            {
                Debug.Log(key - KeyCode.Alpha1);
                chosenDeck = key - KeyCode.Alpha1;
            }
            return;
        }
        switch (key)
        {
            case KeyCode.Q:
                break;
            case KeyCode.W:
                break;
            case KeyCode.E:
                break;
            default:
                break;
        }
    }
}
