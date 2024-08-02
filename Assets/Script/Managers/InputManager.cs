using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager 
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction= null;

    bool _pressed = false;

    public void OnUpdate()
    {
        // ?
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // 입력이 있거나 Action을 참조 하는자가 있다면.
        if (Input.anyKey != false && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null) 
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
