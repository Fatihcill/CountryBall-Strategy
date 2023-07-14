using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public abstract class Building : ObjectModel
{
    [SerializeField]protected bool isProduce, isClicked;

    protected virtual void Awake()
    {
        IsImmortal = false;
        isClicked = false;
    }

    protected virtual void OnMouseUp()
    {
        OnInfo();
        InputManager.Instance.UnSelected.AddListener(OnHide);
    }

    protected virtual void OnInfo()
    {
        GameManager.Instance.uiManager.ShowInformationMenu(
            objectData.name, objectData.preview, isProduce, this);
        isClicked = true;
    }

    protected virtual void OnHide()
    {
        GameManager.Instance.uiManager.HideInfo();
        isClicked = false;
    }

    public virtual void ProduceUnit(int soldierType)
    {
        if (!isProduce) return;
    }
}