using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerClickHandler
{
    public bool isPreset;
    public ItemsData itemData;
    public Image itemIcon;
    public TMP_Text price;
    public TMP_Text itemName;

    [Header("Zinc Card")]
    public bool isZincCard;
    public Sprite unslectedZincIcon;
    private bool isAvailable = true;

    [Header("Protein Card")]
    public TMP_Text proteinLevel;
    // Start is called before the first frame update
    void Start()
    {
        
        SetCard();
        UpdateLevelProtein();
        ProteinManager.instance.OnProteinChanged += UpdateLevelProtein;
    }

    public void UpdateLevelProtein()
    {
        if (itemData.itemCategory == ItemCategory.protein)
        {
            proteinLevel.text = "Lv." + itemData.itemLevel.ToString();

            if (itemData.isUpgradeable == false || itemData.itemLevel == 4)
            {
                //Change color
                gameObject.GetComponent<Image>().color = new Color32(128, 128, 128, 128);
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
    }
    public void SetCard()
    {
        if (isPreset)
        {
            return;
        }
        if (isZincCard)
        {
            if (itemData == null)
            {
                gameObject.GetComponent<Image>().sprite = unslectedZincIcon;
            }
        }
        if (itemData == null)
        {
            return;
        }
        if (itemIcon != null)
        {
            itemIcon.sprite = itemData.itemIcon;
        }
        if (price != null)
        {
            price.text = itemData.itemPrice.ToString();
        }
        if (itemName != null)
        {
            itemName.text = itemData.itemName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAvailable)
        {
            Destroy(gameObject);

        }

    }

    private float lastClickTime = 0f; // Field to store the last click time
    private const float doubleClickThreshold = 0.5f; // Double-click threshold in seconds
    public void OnPointerClick(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time; // Update the last click time

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            // Double-click detected
            if (itemData.itemCategory == ItemCategory.zinc)
            {
                // Add zinc to list on double-click
                
                UiManager.instance.AddZincToList(itemData);
                Debug.Log("Zinc item double-clicked and added to list");
            }
        }
        else
        {
            // Single-click logic
            Debug.Log("Pointer Click");
            if (itemData.itemCategory == ItemCategory.shop)
            {
                BuyItem();
            }
            if (itemData.itemCategory == ItemCategory.zinc)
            {
                UiManager.instance.DisplayZincWindow(itemData);
            }
            if (itemData.itemCategory == ItemCategory.protein)
            {
                ProteinManager.instance.selectedItemsData = itemData;
                ProteinManager.instance.UpgradeAllInCategoryExceptSelected();
            }
            // For single-click, you might not want to immediately add zinc items to the list
            // or handle them differently than double-clicks
        }
        if (itemData.itemCategory == ItemCategory.protein)
        {
            //Update protein
        }
    }


    void BuyItem()
    {
        if (itemData.itemCategory != ItemCategory.shop)
        {
            return;
        }
        if (PlayerInventory.instance.Gold >= itemData.itemPrice)
        {
            PlayerInventory.instance.Gold -= itemData.itemPrice;
            PlayerInventory.instance.AddItem(itemData);
            ShopUI.instance.OnBuyItem();
            isAvailable = false;
            Debug.Log("Item bought");
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    public void RemoveZinc()
    {
        ItemsData selected = itemData;
        
        //Remvoe zinc from list
        UiManager.instance.zincList.Remove(selected);
        PlayerInventory.instance.RemoveItem(selected);

        itemData = null;


        UiManager.instance.OnZincChanged?.Invoke();
    }
}
