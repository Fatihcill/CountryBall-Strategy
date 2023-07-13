
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
    [SerializeField] protected InformationMenu informationMenu;

    [SerializeField] private TextMeshProUGUI powerScoreText;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        pathfinding = GetComponent<Pathfinding>();
        powerSystem = new PowerSystem(0);
    }


    public void UpdatePower(int amount)
    {
        powerSystem.UpdatePower(amount);
        IncreasePowerScore(powerSystem.GetPower());
    }
    //UI CONTROLLER
    public void IncreasePowerScore(int power)
    {
        powerScoreText.text = "Power: " + power.ToString();
    }
    
    public void ShowInformationMenu(string objectName, Sprite preview, bool isProduce, Building building)
    {
        informationMenu.gameObject.SetActive(true);
        informationMenu.ShowInformation(objectName, preview, isProduce, building);
    }

    public void HideInfo()
    {
        informationMenu.gameObject.SetActive(false);
    }
    
    
}
