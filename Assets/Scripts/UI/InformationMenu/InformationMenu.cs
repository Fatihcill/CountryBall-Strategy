using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenu: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField]private Image preview;
    [SerializeField]private TextMeshProUGUI info;
    [SerializeField]private GameObject production;
    private void Awake()
    {
        itemName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        preview = transform.Find("Preview").GetComponent<Image>();
        info = transform.Find("Info").GetComponent<TextMeshProUGUI>();
        production = transform.Find("Production").gameObject;
    }

    public void ShowInformation(string objectName, Sprite objectPreview, string objectInfo, bool isProduce)
    {
        itemName.text = objectName;
        preview.sprite = objectPreview;
        info.text = objectInfo;
        production.SetActive(isProduce);
    }
}
