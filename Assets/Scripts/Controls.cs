using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls Instance;
    public static bool Raise
    {
        get
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
    public static bool Shake
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
    public static bool Fill
    {
        get
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
    public static bool Pause
    {
        get
        {
            return Input.GetKeyDown(KeyCode.P);
        }
    }

    public static bool Up
    {
        get
        {
            return Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.UpArrow);
        }
    }
    public static bool Down
    {
        get
        {
            return Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.DownArrow);
        }
    }
    public static bool Left
    {
        get
        {
            return Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.LeftArrow);
        }
    }
    public static bool Right
    {
        get
        {
            return Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.RightArrow);
        }
    }
    public static bool Click
    {
        get
        {
            return Input.GetMouseButtonUp(0);
        }
    }
    public static bool RightClick
    {
        get
        {
            return Input.GetMouseButtonUp(1);
        }
    }
    public static float MouseWheel
    {
        get
        {
            return Input.mouseScrollDelta.y;
        }
    }
    public static bool Add
    {
        get
        {
            return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        }
    }
    public static bool Assign
    {
        get
        {
            return (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
        }
    }
    public static bool Add2
    {
        get
        {
            return Add && Input.GetKeyDown("2");
        }
    }
    public static bool Assign2
    {
        get
        {
            return Assign && Input.GetKeyDown("2");
        }
    }
    public static bool Add3
    {
        get
        {
            return Add && Input.GetKeyDown("3");
        }
    }
    public static bool Assign3
    {
        get
        {
            return Assign && Input.GetKeyDown("3");
        }
    }
    public static bool Add4
    {
        get
        {
            return Add && Input.GetKeyDown("4");
        }
    }
    public static bool Assign4
    {
        get
        {
            return Assign && Input.GetKeyDown("4");
        }
    }
    public static bool Add5
    {
        get
        {
            return Add && Input.GetKeyDown("5");
        }
    }
    public static bool Assign5
    {
        get
        {
            return Assign && Input.GetKeyDown("5");
        }
    }
    public static bool Add6
    {
        get
        {
            return Add && Input.GetKeyDown("6");
        }
    }
    public static bool Assign6
    {
        get
        {
            return Assign && Input.GetKeyDown("6");
        }
    }
    public static bool Add7
    {
        get
        {
            return Add && Input.GetKeyDown("7");
        }
    }
    public static bool Assign7
    {
        get
        {
            return Assign && Input.GetKeyDown("7");
        }
    }
    public static bool Add8
    {
        get
        {
            return Add && Input.GetKeyDown("8");
        }
    }
    public static bool Assign8
    {
        get
        {
            return Assign && Input.GetKeyDown("8");
        }
    }
    public static bool Add9
    {
        get
        {
            return Add && Input.GetKeyDown("9");
        }
    }
    public static bool Assign9
    {
        get
        {
            return Assign && Input.GetKeyDown("9");
        }
    }
    public static bool Add0
    {
        get
        {
            return Add && Input.GetKeyDown("0");
        }
    }
    public static bool Assign0
    {
        get
        {
            return Assign && Input.GetKeyDown("0");
        }
    }
}