using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected InformationMenu informationMenu;
    [SerializeField] private TextMeshProUGUI powerScoreText;
    private GameObject[] _items;
    
    public GameObject[] InitializeButtons(ObjectPreviewData.ObjectType type, GameObject prefab, Transform parent)
    {
        List<ObjectPreviewData> objects =  GameManager.Instance.database.GetObjectByType(type);
        int length = objects.Count;
        _items = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            _items[i] = Instantiate(prefab, parent);
            _items[i].GetComponent<ProductButton>().SetButton(objects[i].id,
                objects[i].preview, objects[i].name);   
        }
        return _items;
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
