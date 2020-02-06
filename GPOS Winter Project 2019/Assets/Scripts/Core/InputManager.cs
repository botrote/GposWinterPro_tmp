using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void CoordInputEventHandler(Vector2 pos);
    public delegate void InputEventHandler();
    public event InputEventHandler PressQ;
    public event CoordInputEventHandler LeftClickInput;
    public event CoordInputEventHandler RightClickInput;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        RightClickInput += new CoordInputEventHandler(PrintInput);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && RightClickInput != null)
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            RightClickInput(pos);
        }
        if (Input.GetKeyDown(KeyCode.Q) && PressQ != null)
        {
            PressQ();
        }
    }
    void PrintInput(Vector2 pos)
    {
        Debug.Log("Click on" + pos.ToString());
    }
}
