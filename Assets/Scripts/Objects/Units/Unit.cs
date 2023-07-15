using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : ObjectModel
{
    public int speed;
    private bool _changedPos;
    public Cell unitCell;   
    protected readonly Cell Target = new(0, 0);
    protected ObjectModel TargetGameObject;
    protected UnitMovement UnitMove;
    protected AnimManager AnimManager;
    private Animator _anim;
    [FormerlySerializedAs("Neighbours")] [FormerlySerializedAs("test")] public List<Vector2Int> targetNeighbours;
    protected virtual void Awake()
    {
        IsImmortal = false;
        speed = 5;
        unitCell = new Cell(0, 0);
        _anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        AnimManager = new AnimManager(_anim);
        UnitMove = new UnitMovement(ref unitCell, transform, objectData.size, objectData.id, placedObjectIndex, speed, AnimManager);   
        unitCell.pos = Vector2Int.FloorToInt(transform.position);
    }

    protected virtual void Update()
    {
        UnitMove.HandleMovement();
    }
    
    protected void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        InputManager.Instance.OnAction.AddListener(StartAction);
        InputManager.Instance.OnExit.AddListener(StopAction);
    }

    protected void OnMouseUp()
    {
        InputManager.Instance.UnSelected.AddListener(StopAction);
    }

    protected abstract void ActionToTarget();
    
    public void SetTargetPosition() 
    {
        Target.pos = Map.Instance.cellIndicator.currentCell.pos;
        GetTargetObject();
        UnitMove.InitializePathFinding(unitCell, Target);
    }

    private void GetTargetObject()
    {
        bool foundNeighbour = false;
        int targetIndex = Map.Instance.gridData.GetRepresentationIndex(Target.pos);
        TargetGameObject = GameManager.Instance.placementSystem.objectPlacer.GetPlacedObject(targetIndex);
        targetNeighbours = Map.Instance.gridData.GetNeighboursOfPlacedObject(Target.pos, unitCell.pos);
        targetNeighbours = Map.Instance.gridData.ReorderNeighboursByDistance(targetNeighbours, unitCell.pos);
        if (targetNeighbours == null)
        {
            Target.pos = unitCell.pos;
            return;
        }
        foreach (Vector2Int neighbour in targetNeighbours)
        {
            if (Map.Instance.IsCellAvailable(neighbour))
            {
                foundNeighbour = true;
                Target.pos = neighbour;
                break;
            }
        }
        if (!foundNeighbour)
            TargetGameObject = null;
    }
    
    protected void StartAction()
    {
        SetTargetPosition();
        if (TargetGameObject != null)
            ActionToTarget();
        StopAction();
    }

    private void StopAction()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        InputManager.Instance.OnAction.RemoveListener(StartAction);
        InputManager.Instance.OnExit.RemoveListener(StopAction);
        InputManager.Instance.UnSelected.RemoveListener(StopAction);
    }
}