using UnityEngine;

public class Barracks : Building
{
    private Transform _spawnLocation;
    private Vector2 _spawnOffset;
    
    protected override void Awake()
    {
        base.Awake();
        _spawnLocation = transform.GetChild(0);
        this.health = 100;
        this.isProduce = true;
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
        _spawnOffset = Vector2.one * 0.5f;
    }
    
    public override void ProduceUnit(int soldierType)
    {
        base.ProduceUnit(soldierType);
        ObjectPreviewData unitData = GameManager.Instance.database.GetObjectData(soldierType);
        Vector2Int unitPos = Map.Instance.cellIndicator.GetCellWorldPos(_spawnLocation.transform.position);
        GameManager.Instance.placementSystem.StartCreatingObject(soldierType, unitPos);
        /*if(Map.Instance.IsCellOccupied(unitPos, unitData.size))
        {
            Map.Instance.gridData.AddObject(unitPos, unitData.size, unitData.id);
            
            Unit unit = Instantiate(unitData.prefab).GetComponent<Unit>();
            unit.SetObject();
            unit.transform.position = _spawnLocation.transform.position;
        }*/
        //ObjectPool.Get(soldier.prefab);
    }

    private void Move()
    {
        _spawnLocation.transform.position = (Vector2)Map.Instance.currentPos + _spawnOffset;
    }

    protected override void OnInfo()
    {
        base.OnInfo();
        GameManager.Instance.inputManager.OnAction.RemoveAllListeners();
        GameManager.Instance.inputManager.OnAction.AddListener(Move);
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected override void OnHide()
    {
        base.OnHide();
        GameManager.Instance.inputManager.OnAction.RemoveListener(Move);
        _spawnLocation.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
