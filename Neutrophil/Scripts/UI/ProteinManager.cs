using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class ProteinManager : MonoBehaviour
{

    public static ProteinManager instance;
    public int casterCurrentLevel = 0;
    public int offensiveCurrentLevel = 0;
    public int defensiveCurrentLevel = 0;
    public List<ItemsData> caster = new List<ItemsData>();
    public List<ItemsData> offensive = new List<ItemsData>();
    public List<ItemsData> defensive = new List<ItemsData>();
    
    public bool isCanUpgrade = false;
    public ItemsData selectedItemsData;
    public TMP_Text upgradeChanceText;
    public int upgradeChance = 0;
    public Action OnProteinChanged;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateChancesText();
        OnProteinChanged += UpdateChancesText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateChancesText()
    {
        upgradeChanceText.text = "Unused Protein: " + upgradeChance;
    }

    public void UpgradeAllInCategoryExceptSelected()
    {
        if (upgradeChance == 0) return;
        // Determine the category of the selected item
        List<ItemsData> selectedCategory = DetermineCategory(selectedItemsData);

        // Check if the selectedCategory is not null
        if (selectedCategory != null && isCanUpgrade)
        {
            // Iterate through all items in the selected category
            foreach (ItemsData item in selectedCategory)
            {
                // Skip the selected item
                if (item == selectedItemsData) continue;

                // Upgrade logic for the item
                if (item.itemLevel < 4 && item.isUpgradeable)
                {
                    UpgradeItem(item);
                }
            }
            isCanUpgrade = false;
        }
    }

    private List<ItemsData> DetermineCategory(ItemsData item)
    {
        // Check which category the item belongs to and return that category
        if (caster.Contains(item)) return caster;
        if (offensive.Contains(item)) return offensive;
        if (defensive.Contains(item)) return defensive;

        // Return null if the item doesn't belong to any category
        return null;
    }

    private void UpgradeItem(ItemsData item)
    {
        switch (item.itemType)
        {
            case ItemType.caster:
                casterCurrentLevel++;
                break;
            case ItemType.Offensive:
                offensiveCurrentLevel++;
                break;
            case ItemType.Defensive:
                defensiveCurrentLevel++;
                break;
            default:
                break;
        }
        item.itemLevel++;
        selectedItemsData.isUpgradeable = false;
        OnProteinChanged?.Invoke();
        upgradeChance = 0;
    }

    public void OpenProtein()
    {
        //Get first child of the protein window
        upgradeChance = 1;
        UpdateChancesText();
        isCanUpgrade = true;
        Transform proteinWindow = transform.GetChild(0);
        proteinWindow.gameObject.SetActive(true);
    }

    public void CloseProtein()
    {
        //Get first child of the protein window
        Transform proteinWindow = transform.GetChild(0);
        proteinWindow.gameObject.SetActive(false);
    }
    
}
