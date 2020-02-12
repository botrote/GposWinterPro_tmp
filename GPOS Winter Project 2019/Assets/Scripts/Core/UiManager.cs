using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private GameObject Manager;
    private Text text_EXP;
    private Text text_HP;
    private RectTransform select_Arrow;
    private Player player;
    // Start is called before the first frame update
    void Awake()
    {
        Manager = GameObject.Find("Manager");
        text_EXP = gameObject.transform.Find("EXP_Text").gameObject.GetComponent<Text>();
        text_HP = gameObject.transform.Find("HP_Text").gameObject.GetComponent<Text>();
        select_Arrow = gameObject.transform.Find("Select_Arrow").gameObject.GetComponent<RectTransform>();
        select_Arrow.anchoredPosition = new Vector3(-320, -80, 0);
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        Manager.GetComponent<InputManager>().PressKey += new InputManager.InputEventHandler(Move_Arrow);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        text_EXP.text = "EXP : " + player.getExp();
        text_HP.text = "HP : " + player.curHealth + " / " + player.MaxHealth;
    }

    private void Move_Arrow(KeyCode keyCode)
    {
        int idx = keyCode - KeyCode.Alpha1;
        if(idx <= 8 && idx >= 0)
        {
            select_Arrow.anchoredPosition = new Vector3(-320 + (idx*80), -80, 0);
            return;
        }
    }
}
