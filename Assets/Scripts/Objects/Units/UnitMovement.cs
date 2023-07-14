using System.Collections.Generic;
using UnityEngine;

public class UnitMovement
{
    //Controls
    public bool IsMoving;
    private List<Cell> _pathVectorList;
    private Vector3 _nextCellPos, _moveDir;
    private int _currentPathIndex;
    private bool _changedPos;

    //Unit Values
    private readonly Cell _unitCell;
    private readonly Vector2Int _size;
    private readonly int _id;
    private readonly int _index;
    private readonly int _speed;
    private readonly Transform _transform;
    private AnimManager _animManager;
    
    //Rotate
    private SpriteRenderer _spriteRenderer;
    private float _angle;
    
    public UnitMovement (ref Cell unitCell, Transform transform, Vector2Int size, int id, int index, int speed, AnimManager anim)
    {
        _unitCell = unitCell;
        _size = size;
        _id = id;
        _index = index;
        _speed = speed;
        _transform = transform;
        _animManager = anim;
        _spriteRenderer = _transform.GetComponent<SpriteRenderer>();
        IsMoving = false;
    }
    
    public void InitializePathFinding(Cell startCell, Cell targetCell)
    {
        IsMoving = true;
        _changedPos = false;
        _currentPathIndex = 0;
        _pathVectorList = GameManager.Instance.pathfinding.FindPath(startCell, targetCell);
        if (_pathVectorList.Count == 0)
            IsMoving = false;
    }
    
    public void HandleMovement() 
    {
        if (_pathVectorList?.Count > 0 && _currentPathIndex < _pathVectorList.Count)
        {
            _nextCellPos = _pathVectorList[_currentPathIndex].worldPos;
            MovementAction();
        }
    }
    
    private void MovementAction()
    {
        float distance = Vector3.Distance(_transform.position, _nextCellPos);
        if (!_changedPos && !Map.Instance.IsCellAvailable(_pathVectorList[_currentPathIndex].pos)) // RECALCULATE PATH IF CELL IS NOT AVAILABLE
        {
            InitializePathFinding(_unitCell, _pathVectorList[_pathVectorList.Count - 1]);
        }
        else if (distance > 0.1f)
        {
            _moveDir = (_nextCellPos - _transform.position).normalized;
            _transform.position += _moveDir * (_speed * Time.deltaTime);
            if (!_changedPos)
            {
                IsMoving = true;
                _animManager.SetAnim(AnimationTypes.Walk, IsMoving);
                RotateTransform();
                UpdatePos(_pathVectorList[_currentPathIndex].pos);
            }
        } 
        else
        {
            _currentPathIndex++;
            _changedPos = false;
            if (_currentPathIndex >= _pathVectorList.Count) // Arrived the Target
            {
                IsMoving = false;
                _animManager.SetAnim(AnimationTypes.Walk, IsMoving);
                _currentPathIndex = 0;
                _pathVectorList = null;
            }
        }
        
    }
    
    private void UpdatePos(Vector2Int currentPos)
    {
        //Debug.Log($"delete {_unitCell.pos} add {currentPos}");
        Map.Instance.gridData.RemoveObjectAt(_unitCell.pos);
        Map.Instance.gridData.AddObject(currentPos, _size, _id, _index);
        _unitCell.pos = currentPos;
        _changedPos = true;
    }

    private void RotateTransform()
    {
        _angle = Mathf.Atan2(_moveDir.y, _moveDir.x) * Mathf.Rad2Deg;
        if (_angle >= -90f && _angle <= 90f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            _angle += 180f;
        }
        _transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }
}
