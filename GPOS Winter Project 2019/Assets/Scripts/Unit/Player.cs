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
    
    public struct DeckInfo {public int cost; public bool isUnlocked; public int unlockCost; }

    protected GameObject Manager;
    protected Deck[] deck;
    //protected bool[] deckUnlocked;
    //protected int[] unlockCost;
    protected DeckInfo[] deckInfo;
    protected int chosenDeck;

    // Start is called before the first frame update
    protected void Awake()
    {
        Exp = 4000;
        deck = new Deck[12];
        deckInfo = new DeckInfo[12];
        //eckUnlocked = new bool[9];
        //unlockCost = new int[9];
        string deckname;
        int decknotch;
        NPC.getNameAndCost<SkeletonB>(out decknotch, out deckname, out deckInfo[0].unlockCost);
        deck[0] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<SkeletonS>(out decknotch, out deckname, out deckInfo[1].unlockCost);
        deck[1] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Orc>(out decknotch, out deckname, out deckInfo[2].unlockCost);
        deck[2] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Ghost>(out decknotch, out deckname, out deckInfo[3].unlockCost);
        deck[3] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Lich>(out decknotch, out deckname, out deckInfo[4].unlockCost);
        deck[4] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Troll>(out decknotch, out deckname, out deckInfo[5].unlockCost);
        deck[5] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Goblin>(out decknotch, out deckname, out deckInfo[6].unlockCost);
        deck[6] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Devil>(out decknotch, out deckname, out deckInfo[7].unlockCost);
        deck[7] = new Deck(deckname, decknotch);
        NPC.getNameAndCost<Dragon>(out decknotch, out deckname, out deckInfo[8].unlockCost);
        deck[8] = new Deck(deckname, decknotch);
        ISkill.getCost<FireballSkill>(out decknotch, out deckInfo[9].unlockCost);
        deck[9] = new Deck(new FireballSkill(), decknotch);
        ISkill.getCost<CommandSkill>(out decknotch, out deckInfo[10].unlockCost);
        deck[10] = new Deck(new CommandSkill(), decknotch);
        NPC.getNameAndCost<Flag>(out decknotch, out deckname, out deckInfo[11].unlockCost);
        deck[11] = new Deck(deckname, decknotch);

        chosenDeck = 0;

        for(int i = 0; i < deck.Length; i++)
        {
            if (i < 2) deckInfo[i].isUnlocked = true;
            else if (i == 9) deckInfo[i].isUnlocked = true;
            else deckInfo[i].isUnlocked = false;
            deckInfo[i].cost = deck[i].getcost();
        }
        
        Manager = GameObject.Find("Manager");
        Manager.GetComponent<InputManager>().RightClickInput += new InputManager.CoordInputEventHandler(Move);
        Manager.GetComponent<InputManager>().LeftClickInput += new InputManager.CoordInputEventHandler(UseDeck);
        Manager.GetComponent<InputManager>().PressKey += new InputManager.InputEventHandler(UseSkill);
        Manager.GetComponent<InputManager>().WheelInput += new InputManager.WheelEventHandler(UseSkill_Wheel);
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
        if(deckInfo[chosenDeck].isUnlocked == true)
        {
            if (subtractExp(deck[chosenDeck].getcost()))
            {
                int squadNum;
                if(chosenDeck == 0)
                    squadNum = 3;
                else if (chosenDeck == 1)
                    squadNum = 4;
                else if (chosenDeck == 2)
                    squadNum = 2;
                else
                    squadNum = 1;

                for(int i = 0; i < squadNum; i++)
                    deck[chosenDeck].useDeck(pos);
            }
            else
            {
                Debug.Log("Unsufficient Exp! : " + Exp);
            }
        }
        else
        {
            if (subtractExp(deckInfo[chosenDeck].unlockCost))
            {
                deckInfo[chosenDeck].isUnlocked = true;
            }
            else
            {
                Debug.Log("Unsufficient Exp! : " + Exp);
            }
        }
        
    }

    protected void UseSkill(KeyCode key)
    {
        if(key <= KeyCode.Alpha9 && key >= KeyCode.Alpha1)
        {
            if (deck[key - KeyCode.Alpha1] != null)
            {
                chosenDeck = key - KeyCode.Alpha1;
            }
            return;
        }
        switch (key)
        {
            case KeyCode.Q:
                chosenDeck = 9;
                break;
            case KeyCode.W:
                chosenDeck = 10;
                break;
            case KeyCode.E:
                chosenDeck = 11;
                break;
            default:
                break;
        }
    }

    protected void UseSkill_Wheel(bool isUP)
    {
        if(isUP)
        {
            chosenDeck++;
            if(chosenDeck > deck.Length - 1)
                chosenDeck = 0;
        }
        else
        {
            chosenDeck--;
            if(chosenDeck < 0)
                chosenDeck = deck.Length - 1;
        }
    }

    public int GetSelectedUnitIdx()
    {
        return chosenDeck;
    }

    public DeckInfo[] ShowDeckInfo()
    {
        return deckInfo;
    }
}
