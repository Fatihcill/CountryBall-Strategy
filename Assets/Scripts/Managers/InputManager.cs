using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [SerializeField] private Camera sceneCamera;
    private Vector2 _lastPosition;
    public UnityEvent OnClicked, OnExit, OnAction;
    public UnityEvent UnSelected;

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        Instance = this;
    }

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
            UnSelected?.Invoke();
            UnSelected?.RemoveAllListeners();
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
