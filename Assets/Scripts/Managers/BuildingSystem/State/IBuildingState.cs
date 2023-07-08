using UnityEngine;

public interface IBuildingState
{
    void EndState();
    void OnAction(Vector3Int cellPos);
    void UpdateState(Vector3Int cellPos);
}