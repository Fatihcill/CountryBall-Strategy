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
    public UnityEvent OnClicked, OnExit, OnAction, UnSelected;

    private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        Instance = this;
    }

    public Vector2 GetSelectedMapPosition()
    {
        Vector2 mousePosition =sceneCamera.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
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
