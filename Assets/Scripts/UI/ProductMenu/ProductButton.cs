using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    public Building producer;
    public int id;
    
    public void SetButton(int unitId, Sprite preview, string text)
    {
        GetComponent<Image>().sprite = preview;
        GetComponentInChildren<TextMeshProUGUI>().text = text;
        id = unitId;
    }

    public void PlacementAction()
    {
        GameManager.Instance.placementSystem.StartPlacement(id);
    }
    
    public void ProductionAction()
    {
        if (producer)
            producer.ProduceUnit(this.id);
        else
            Debug.LogWarning("Producer is null");
    }
}
