using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProteinCard : MonoBehaviour
{

    public ItemsData item;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image icon;
    public Image[] stars;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickItems);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //On button click, upgrade the item
    public void UpgradeItem()
    {
        item.itemLevel++;
        SetCard();
    }

    public void ClickItems()
    {
        ConfirmationDialog.Instance.ShowDialog(() =>
        {
            UpgradeItem();
        });
    }



    public void SetCard()
    {
        
        nameText.text = item.itemName;
        descriptionText.text = item.itemDescriptionLevel[item.itemLevel - 1];
        icon.sprite = item.itemIcon;
        Debug.Log(item.itemName);
        for (int i = 0; i < item.itemLevel; i++)
        {
            switch (item.itemLevel)
            {
                case 1:
                    stars[i].color = Color.yellow;
                    break;
                case 2:
                    stars[i].color = Color.green;
                    break;
                case 3:
                    stars[i].color = Color.blue;
                    break;
                case 4:
                    stars[i].color = Color.red;
                    break;
                case 5:
                    stars[i].color = Color.magenta;
                    break;
            }
        }
    }
}
