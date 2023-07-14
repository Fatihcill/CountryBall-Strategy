
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager inputManager;
    public PlacementSystem placementSystem;
    public ObjectsDatabaseManager database;
    public Pathfinding pathfinding;
    public PowerSystem powerSystem;
    public UIManager uiManager;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        Instance = this;
        pathfinding = GetComponent<Pathfinding>();
        powerSystem = new PowerSystem(0);
    }
    
    public void UpdatePower(int amount)
    {
        powerSystem.UpdatePower(amount);
        uiManager.IncreasePowerScore(powerSystem.GetPower());
    }
}
