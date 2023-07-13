using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    private Vector2 _lastPosition;
    public UnityEvent OnClicked, OnExit, OnAction, OnHideInfo;

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    public Vector2 GetSelectedMapPosition()
    {
        Vector2 mousePosition =sceneCamera.ScreenToWorldPoint(Input.mousePosition);;
        Vector2 selectedMapPosition = new Vector2(mousePosition.x, mousePosition.y);
        return selectedMapPosition;
    }
    
    private void Update()
    {
        if (IsPointerOverUI()) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
            OnHideInfo?.Invoke();
            OnHideInfo?.RemoveAllListeners();
        }
        if (Input.GetMouseButton(1))
        {
            OnAction?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnExit?.Invoke();
        }
    }
}
