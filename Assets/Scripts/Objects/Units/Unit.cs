using UnityEngine;

public abstract class Unit : ObjectModel
{
    public int speed;
    private bool _changedPos;
    public Cell unitCell;   
    private readonly Cell _target = new(0, 0);
    protected ObjectModel TargetGameObject;
    protected UnitMovement UnitMove;
    protected AnimManager AnimManager;
    private Animator _anim;
    
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
        UnitMove = new UnitMovement(ref unitCell, transform, ObjectData.size, ObjectData.id, placedObjectIndex, speed, AnimManager);   
    }

    protected virtual void Update()
    {
        UnitMove.HandleMovement();
    }
    
    protected void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        unitCell.pos = Vector2Int.FloorToInt(transform.position);
        GameManager.Instance.inputManager.OnAction.AddListener(StartAction);
        GameManager.Instance.inputManager.OnExit.AddListener(StopAction);
    }

    protected void OnMouseUp()
    {
        GameManager.Instance.inputManager.UnSelected.AddListener(StopAction);
    }

    protected abstract void ActionToTarget();
    
    public void SetTargetPosition() 
    {
        _target.pos = Map.Instance.cellIndicator.currentCell.pos;
        int targetIndex = Map.Instance.gridData.GetRepresentationIndex(_target.pos);
        TargetGameObject = GameManager.Instance.placementSystem.objectPlacer.GetPlacedObject(targetIndex);
        UnitMove.InitializePathFinding(unitCell, _target);
    }

    protected void StartAction()
    {
        SetTargetPosition();
        Debug.Log(TargetGameObject);
        if (TargetGameObject != null)
            ActionToTarget();
        StopAction();
    }

    private void StopAction()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.Instance.inputManager.OnAction.RemoveListener(StartAction);
        GameManager.Instance.inputManager.OnExit.RemoveListener(StopAction);
        GameManager.Instance.inputManager.UnSelected.RemoveListener(StopAction);
    }
}