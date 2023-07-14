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
        GameManager.Instance.inputManager.UnSelected.AddListener(OnHide);
    }

    protected virtual void OnInfo()
    {
        GameManager.Instance.uiManager.ShowInformationMenu(
            ObjectData.name, ObjectData.preview, isProduce, this);
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