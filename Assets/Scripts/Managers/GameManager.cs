
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlacementSystem placementSystem;
    public ObjectsDatabaseManager database;
    public Pathfinding pathfinding;
    public UIManager uiManager;
    private PowerSystem _powerSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        Instance = this;
        pathfinding = GetComponent<Pathfinding>();
        _powerSystem = new PowerSystem(0);
    }
    
    public void UpdatePower(int amount)
    {
        _powerSystem.UpdatePower(amount);
        uiManager.IncreasePowerScore(_powerSystem.GetPower());
    }
}
