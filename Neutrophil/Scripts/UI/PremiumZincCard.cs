using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class PremiumZincCard : MonoBehaviour, IPointerClickHandler
{
    public ZincUI zincUI;
    public ItemsData itemData;
    // Start is called before the first frame update
    void Start()
    {
        //Get its parent component
        zincUI = GetComponentInParent<ZincUI>();
        UpdateZincCard();
        zincUI.onZincChanged += UpdateZincCard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        zincUI.SetDisplay(itemData);
        zincUI.selectedItemsData = itemData;
    }

    void UpdateZincCard()
    {
        //Update the zinc card
        if (itemData.isItemBought)
        {
            gameObject.GetComponent<Image>().sprite = itemData.itemActiveIcon;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = itemData.itemIcon;
        }
    }


}
