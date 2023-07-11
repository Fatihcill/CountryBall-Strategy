
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager inputManager;
    public PlacementSystem placementSystem;
    public ObjectsDatabaseManager database;
    public Pathfinding pathfinding;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        pathfinding = GetComponent<Pathfinding>();
    }
}
