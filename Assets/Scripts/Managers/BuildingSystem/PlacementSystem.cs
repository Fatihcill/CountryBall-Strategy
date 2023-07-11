using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ObjectPlacer objectPlacer;
    [SerializeField] Map map;
    private IBuildingState _buildingState;
    
    private void Start()
    {
        StopPlacement();
    }

    private void Update()
    {
        if (_buildingState != null)
            _buildingState.UpdateState(map.cellIndicator.currentCell.pos);
    }
    
    public void StartPlacement(int id)
    {
        StopPlacement();
        _buildingState = new PlacementState(id, objectPlacer, map.cellIndicator);
        inputManager.OnClicked.AddListener(PlaceStructure);
        inputManager.OnExit.AddListener(StopPlacement);
    }
    public void StartCreatingObject(int id, Vector2Int spawnPos)
    {
        StopPlacement();
        _buildingState = new PlacementState(id, objectPlacer, map.cellIndicator);
        _buildingState.OnAction(spawnPos);
        StopPlacement();
    }
    public void StartRemoving()
    {
        StopPlacement();
        _buildingState = new RemovingState(objectPlacer, map.cellIndicator);
        inputManager.OnClicked.AddListener(PlaceStructure);
        inputManager.OnExit.AddListener(StopPlacement);
    }
    
    public void StopPlacement()
    {
        if (_buildingState == null) return;
        _buildingState.EndState();
        inputManager.OnClicked.RemoveListener(PlaceStructure);
        inputManager.OnExit.RemoveListener(StopPlacement);
        _buildingState = null;
    }
 
    private void PlaceStructure()
    {
        _buildingState.OnAction(map.cellIndicator.currentCell.pos);
        StopPlacement();
    }
    


}
