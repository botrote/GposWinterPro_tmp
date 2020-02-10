using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void CoordInputEventHandler(Vector2 pos);
    public delegate void InputEventHandler(KeyCode key);
    public event InputEventHandler PressKey;
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
        if (Input.GetKeyDown(KeyCode.Q) && PressKey != null)
        {
            PressKey(KeyCode.Q);
        }
        if (Input.GetKeyDown(KeyCode.W) && PressKey != null)
        {
            PressKey(KeyCode.W);
        }
        if (Input.GetKeyDown(KeyCode.E) && PressKey != null)
        {
            PressKey(KeyCode.E);
        }
    }
    void PrintInput(Vector2 pos)
    {
        Debug.Log("Click on" + pos.ToString());
    }
}
