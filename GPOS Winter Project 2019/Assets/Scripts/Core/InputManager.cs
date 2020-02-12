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

    public Vector2 getMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && LeftClickInput != null)
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            LeftClickInput(pos);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && RightClickInput != null)
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && PressKey != null)
        {
            PressKey(KeyCode.Alpha1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && PressKey != null)
        {
            PressKey(KeyCode.Alpha2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && PressKey != null)
        {
            PressKey(KeyCode.Alpha3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && PressKey != null)
        {
            PressKey(KeyCode.Alpha4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && PressKey != null)
        {
            PressKey(KeyCode.Alpha5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && PressKey != null)
        {
            PressKey(KeyCode.Alpha6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && PressKey != null)
        {
            PressKey(KeyCode.Alpha7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && PressKey != null)
        {
            PressKey(KeyCode.Alpha8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && PressKey != null)
        {
            PressKey(KeyCode.Alpha9);
        }
    }
    void PrintInput(Vector2 pos)
    {
        Debug.Log("Click on" + pos.ToString());
    }
}
