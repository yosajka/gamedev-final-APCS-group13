using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    void OnLeftClick();

    void OnMouseEnterHover();
    void OnMouseExistHover();

    void OnRightClickDown();
    void OnRightClickUp();
}
