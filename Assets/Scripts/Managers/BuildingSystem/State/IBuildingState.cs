using UnityEngine;

public interface IBuildingState
{
    void EndState();
    void OnAction(Vector2Int cellPos);
    void UpdateState(Vector2Int cellPos);
}