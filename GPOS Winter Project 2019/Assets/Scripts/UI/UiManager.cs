using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private GameObject Manager;
    private Text text_EXP;
    private Text text_HP;
    private int selectedDeck;
    private RectTransform select_Arrow;
    private GameObject deckImageObj;
    public GameObject[] Deck_Images;
    private Player player;
    private Player.DeckInfo[] deckInfo;
    // Start is called before the first frame update
    void Awake()
    {
        Manager = GameObject.Find("Manager");
        text_EXP = gameObject.transform.Find("EXP_Text").gameObject.GetComponent<Text>();
        text_HP = gameObject.transform.Find("HP_Text").gameObject.GetComponent<Text>();
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
        text_EXP.text = "EXP : " + player.getExp();
        text_HP.text = "HP : " + player.curHealth + " / " + player.MaxHealth;
        selectedDeck = player.GetSelectedUnitIdx();
        if(selectedDeck < 9)
        {
            select_Arrow.rotation = Quaternion.Euler(0,0,180);
            select_Arrow.anchoredPosition = new Vector3(-320 + (selectedDeck*80), -80, 0);
        }
        else
        {
            select_Arrow.rotation = Quaternion.Euler(0,0,0);
            select_Arrow.anchoredPosition = new Vector3(-320 + ((selectedDeck - 9)*80), -365, 0);
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
            if(deckInfo[idx].isUnlocked == true)
            {
                curText.text = deckInfo[idx].cost.ToString()+ " exp";
                curLockImage.enabled = false;
            }
            else
            {
                curText.text = "Unlock : \n" + deckInfo[idx].unlockCost + " exp";
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
