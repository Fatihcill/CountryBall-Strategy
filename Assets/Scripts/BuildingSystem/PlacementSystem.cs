using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] public ObjectPlacer objectPlacer;
    private IBuildingState _buildingState;
    private CellIndicator _cellIndicator;
    
    private void Start()
    {
        StopPlacement();
        _cellIndicator = Map.Instance.cellIndicator;;
    }

    private void Update()
    {
        if (_buildingState != null)
            _buildingState.UpdateState(_cellIndicator.currentCell.pos);
    }
    
    public void StartPlacement(int id)
    {
        StopPlacement();
        _buildingState = new PlacementState(id, objectPlacer, _cellIndicator);
         InputManager.Instance.OnClicked.AddListener(PlaceStructure);
         InputManager.Instance.OnExit.AddListener(StopPlacement);
    }
    
    public void StartCreatingObject(int id, Vector2Int spawnPos)
    {
        StopPlacement();
        _buildingState = new PlacementState(id, objectPlacer, _cellIndicator);
        _buildingState.OnAction(spawnPos);
        StopPlacement();
    }
    
    public void StartRemoving()
    {
        StopPlacement();
        _buildingState = new RemovingState(objectPlacer, _cellIndicator);
         InputManager.Instance.OnClicked.AddListener(PlaceStructure);
         InputManager.Instance.OnExit.AddListener(StopPlacement);
    }
    
    public void StopPlacement()
    {
        if (_buildingState == null) return;
        _buildingState.EndState();
         InputManager.Instance.OnClicked.RemoveListener(PlaceStructure);
         InputManager.Instance.OnExit.RemoveListener(StopPlacement);
        _buildingState = null;
    }
 
    private void PlaceStructure()
    {
        _buildingState.OnAction(_cellIndicator.currentCell.pos);
        StopPlacement();
    }
}
