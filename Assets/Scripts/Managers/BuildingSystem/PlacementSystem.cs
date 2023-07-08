using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//todo Refactor this class
public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private BuildingPlacer buildingPlacer;
    [SerializeField] private ObjectsDatabaseManager objectManager;
    [SerializeField] Map map;
    private IBuildingState _buildingState;
    
    private void Start()
    {
        StopPlacement();
    }

    private void Update()
    {
        if (_buildingState != null)
            _buildingState.UpdateState(map.cellIndicator.cellPos);
    }
    
    public void StartPlacement(int id)
    {
        StopPlacement();
        _buildingState = new PlacementState(id, map.GridData, objectManager, buildingPlacer, map.cellIndicator);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        _buildingState = new RemovingState(buildingPlacer, map.GridData, map.cellIndicator);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }
    
    private void StopPlacement()
    {
        if (_buildingState == null) return;
        _buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        _buildingState = null;
    }
 
    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI()) return;
        _buildingState.OnAction(map.cellIndicator.cellPos);
        StopPlacement();
    }
}
