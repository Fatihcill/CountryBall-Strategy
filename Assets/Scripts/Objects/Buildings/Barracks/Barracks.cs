using UnityEngine;

public class Barracks : Building
{
    private Transform _spawnLocation;
    private Vector2 _spawnOffset;
    
    protected override void Awake()
    {
        base.Awake();
        _spawnLocation = transform.GetChild(0);
        this.MaxHealth = 100;
        this.isProduce = true;
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
        _spawnOffset = Vector2.one * 0.5f;
    }
    
    public override void ProduceUnit(int soldierType)
    {
        base.ProduceUnit(soldierType);
        ObjectPreviewData unitData = GameManager.Instance.database.GetObjectData(soldierType);
        Vector2Int unitPos = Vector2Int.FloorToInt(_spawnLocation.transform.position);
        GameManager.Instance.placementSystem.StartCreatingObject(soldierType, unitPos);
    }

    private void MoveSpawnPoint()
    {
        _spawnLocation.transform.position = (Vector2)Map.Instance.currentPos + _spawnOffset;
    }

    protected override void OnInfo()
    {
        base.OnInfo();
        GameManager.Instance.inputManager.OnAction.RemoveAllListeners();
        GameManager.Instance.inputManager.OnAction.AddListener(MoveSpawnPoint);
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected override void OnHide()
    {
        base.OnHide();
        GameManager.Instance.inputManager.OnAction.RemoveListener(MoveSpawnPoint);
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
