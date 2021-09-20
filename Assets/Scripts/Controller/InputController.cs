using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Vector3 _firstPressPos;
    Vector3 _secondPressPos;
    Vector3 _currentSwipe;
    Vector3 _mOffset;
    private IClickable _previousHover;
    private IClickable _currentClickable;

    int _layer;
    void Start()
    {
        _layer = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        // If on Hover Scope(Not press anything)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(_layer);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
            {
                Debug.Log("layer" + _layer);
                IClickable clickable = hit.transform.GetComponent<IClickable>();
                if (clickable != _previousHover)
                {
                    if (clickable != null)
                    {
                        clickable.OnMouseEnterHover();
                    }
                    if (_previousHover != null && _currentClickable != _previousHover)
                        _previousHover.OnMouseExistHover();
                    _previousHover = clickable;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
            {
                Debug.Log("fuck");
                IClickable clickable = hit.transform.GetComponent<IClickable>();
                if (clickable != null)
                {
                    if (clickable != _currentClickable && _currentClickable != null)
                    {
                        _currentClickable.OnMouseExistHover();
                    }
                    clickable.OnLeftClick();
                    _currentClickable = clickable;
                }
                else ResetCurrentClickable();
            }
            else ResetCurrentClickable();
        }

        if (_currentClickable != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _currentClickable.OnRightClickDown();

            }
            if (Input.GetMouseButtonUp(1))
            {
                _currentClickable.OnRightClickUp();
            }
        }

    }

    public void ResetCurrentClickable()
    {
        if (_currentClickable != null)
        {
            _currentClickable.OnMouseExistHover();
            _currentClickable = null;
        }
    }



}
