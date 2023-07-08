using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    private Vector2 lastPosition;
    public event Action OnClicked, OnExit;

    public Vector2 GetSelectedMapPosition()
    {
        Vector2 mousePosition =sceneCamera.ScreenToWorldPoint(Input.mousePosition);;
        Vector2 selectedMapPosition = new Vector2(mousePosition.x, mousePosition.y);
        return selectedMapPosition;
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnExit?.Invoke();
        }
    }
}
