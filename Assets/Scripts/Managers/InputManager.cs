using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum Type
{
    DeleteActıon,
    PlaceActıon,
    None
}
public class InputManager : MonoBehaviour
{
    public static Type CurrentType = Type.None;
    [SerializeField] private Camera sceneCamera;
    private Vector2 _lastPosition;
    public UnityEvent OnClicked, OnExit;
    [SerializeField] protected InformationMenu informationMenu;

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
    public void ShowInformationMenu(string objectName, Sprite preview, string description, bool isProduce)
    {
        informationMenu.gameObject.SetActive(true);
        informationMenu.ShowInformation(objectName, preview, description, isProduce);
    }

    public void HideInfo()
    {
        informationMenu.gameObject.SetActive(false);
    }
}
