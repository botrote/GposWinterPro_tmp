using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private GameObject Manager;
    private Text text_EXP;
    private Text text_HP;
    private Text text_Wave;
    private int selectedDeck;
    private RectTransform select_Arrow;
    private GameObject deckImageObj;
    public GameObject[] Deck_Images;
    public GameObject EXP;
    public GameObject HP;
    public GameObject Wave;
    private Player player;
    private Player.DeckInfo[] deckInfo;
    // Start is called before the first frame update
    void Awake()
    {
        Manager = GameObject.Find("Manager");
        EXP=gameObject.transform.Find("EXP_Bar").gameObject;
        text_EXP = EXP.transform.Find("EXP_Text").gameObject.GetComponent<Text>();
        HP=gameObject.transform.Find("HP_Bar").gameObject;
        text_HP = HP.transform.Find("HP_Text").gameObject.GetComponent<Text>();
        Wave=gameObject.transform.Find("Wave_Marker").gameObject;
        text_Wave =Wave.transform.Find("Wave_Text").gameObject.GetComponent<Text>();
        select_Arrow = gameObject.transform.Find("Select_Arrow").gameObject.GetComponent<RectTransform>();
        //select_Arrow.anchoredPosition = new Vector3(-320, -80, 0);
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        deckImageObj = gameObject.transform.Find("Deck_Images").gameObject;
        Deck_Images = new GameObject[12];
        for(int idx = 0; idx < Deck_Images.Length; idx++)
        {
            Deck_Images[idx] = deckImageObj.transform.GetChild(idx).gameObject;
        }
        ShowDeck();
        //Manager.GetComponent<InputManager>().PressKey += new InputManager.InputEventHandler(Move_Arrow_Key);
        //Manager.GetComponent<InputManager>().WheelInput += new InputManager.WheelEventHandler(Move_Arrow_Wheel);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        text_Wave.text = (Manager.gameObject.GetComponent<WaveManager>().getWaveNum()+1).ToString();
        text_EXP.text =  player.getExp().ToString();
        if(player.getExp()<=200)
        {
            EXP.transform.Find("EXP_Gauge").gameObject.GetComponent<Image>().color= new Color(1, 1, 0);
            EXP.transform.Find("EXP_Gauge").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1002*((float)player.getExp()/200), 22);
        }
        else
        {
            EXP.transform.Find("EXP_Gauge").gameObject.GetComponent<Image>().color= new Color(1, 180f/255, 0);
        }
        text_HP.text =  player.curHealth.ToString();
        HP.transform.Find("HP_Gauge").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1020*((float)player.curHealth/player.MaxHealth), 23);
        selectedDeck = player.GetSelectedUnitIdx();
        if(selectedDeck < 9)
        {
            select_Arrow.rotation = Quaternion.Euler(0,0,0);
            select_Arrow.anchoredPosition = new Vector3(-455 + (selectedDeck*80), -250, 0);
        }
        else
        {
            select_Arrow.rotation = Quaternion.Euler(0,0,0);
            select_Arrow.anchoredPosition = new Vector3(295 + ((selectedDeck - 9)*80), -250, 0);
        }
        ShowDeck();
    }

    void ShowDeck()
    {
        deckInfo = player.ShowDeckInfo();
        Text curText;
        Image curLockImage;
        for(int idx = 0; idx < Deck_Images.Length; idx++)
        {
            curText = Deck_Images[idx].transform.Find("Cost").gameObject.GetComponent<Text>();
            curLockImage = Deck_Images[idx].transform.Find("Lock").gameObject.GetComponent<Image>();
            
            if(deckInfo[idx].leftCool > 0)
            {
                curText.text = "Cool\n"+deckInfo[idx].leftCool.ToString("N1");
                curLockImage.enabled = true;        
            }
            else if(deckInfo[idx].isUnlocked == true)
            {
                curText.text = "Cost\n"+deckInfo[idx].cost.ToString()+ " exp";
                curLockImage.enabled = false;
            }
            else
            {
                curText.text = "Unlock\n" + deckInfo[idx].unlockCost + " exp";
                curLockImage.enabled = true;
            }
        }

    }

/*
    private void Move_Arrow_Key(KeyCode keyCode)
    {
        int idx = keyCode - KeyCode.Alpha1;
        if(idx <= 8 && idx >= 0)
        {
            arrowIdx = idx;
            return;
        }
    }

    private void Move_Arrow_Wheel(bool isUP)
    {
        if(isUP)
        {
            arrowIdx++;
            if(arrowIdx > 8)
                arrowIdx = 0;
        }
        else
        {
            arrowIdx--;
            if(arrowIdx < 0)
                arrowIdx = 8;
        }
    }
*/
}
