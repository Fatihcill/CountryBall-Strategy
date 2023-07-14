using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenu: MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI itemName;
    [SerializeField]private Image preview;
    [SerializeField]private GameObject production;
    [SerializeField]private GameObject []productionButtons;
    [SerializeField]private GameObject buttonPrefab;
    [SerializeField]private UIManager uiManager;
    private void Awake()
    {
        itemName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        preview = transform.Find("Preview").GetComponent<Image>();
        production = transform.Find("Production").gameObject;
        uiManager = transform.parent.GetComponent<UIManager>();

        productionButtons = uiManager.InitializeButtons(ObjectPreviewData.ObjectType.Unit, buttonPrefab, production.transform);
        gameObject.SetActive(false);
    }

    public void ShowInformation(string objectName, Sprite objectPreview, bool isProduce, Building building = null)
    {
        itemName.text = objectName;
        preview.sprite = objectPreview;
        production.SetActive(isProduce);
        if (isProduce)
        {
            foreach (var button in productionButtons)
            {
                button.GetComponent<ProductButton>().producer = building;
            }
        }
    }
}
