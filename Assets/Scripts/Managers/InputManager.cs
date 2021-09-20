using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager _instance;

    public delegate void InputEvent();
    public static event InputEvent OnPressUp;
    public static event InputEvent OnPressDown;
    public static event InputEvent OnTap;
    void Start()
    {
        if (_instance == null) _instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (OnPressUp != null) OnPressUp();
        }
    }

}
