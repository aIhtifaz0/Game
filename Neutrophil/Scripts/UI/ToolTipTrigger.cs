using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    ItemsData itemData;
    public string header;
    [TextArea(3, 10)]
    public string content;

    void Start()
    {
        
    }


    void Update()
    {
        while (itemData == null)
        {
            GetItemData();
        }
    }

    void GetItemData()
    {
        itemData = GetComponent<CardUI>().itemData;
        header = itemData.itemName;
        content = itemData.itemDescription;
        Debug.Log("Item data set to :" + itemData.itemName);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData.itemCategory == ItemCategory.protein)
        {
            int itemLevel = itemData.itemLevel;
            TooltipSystem.Show(header, content, itemData.itemDescriptionLevel[itemLevel - 1]);
        }
        else
        {
            TooltipSystem.Show(header, content, itemData.itemPrice.ToString());
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
