using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance; // Singleton instance

    [SerializeField]
    private int gold; // Private variable

    public int Gold // Public property
    {
        get { return gold; } // Getter
        set { gold = value; } // Setter
    }

    
    public event Action OnItemsChanged;
    [Header("Stats")]
    public List<ItemsData> itemsInInventory = new List<ItemsData>(); // List of items in inventory
    public List<ItemsData> SkillsInInventory = new List<ItemsData>(); // List of skills in inventory
    public List<ItemsData> ZincInUse = new List<ItemsData>(); // List of zinc in use

    void Awake()
    {
        if (instance == null) // If instance is null
        {
            instance = this; // Set instance to this
        }
        else if (instance != this) // If instance is not this
        {
            Destroy(gameObject); // Destroy this
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        OnItemsChanged += CalculateStats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(ItemsData item)
    {
        if (item.itemCategory == ItemCategory.zinc)
        {
            ZincInUse.Add(item);
        }
        else if (item.itemCategory == ItemCategory.protein)
        {
            SkillsInInventory.Add(item);
        }
        else
        {
            itemsInInventory.Add(item);
            PlayerStats ps = GetComponent<PlayerStats>();
            ps.playerStatsTotal.Add(item);
        }
        OnItemsChanged?.Invoke(); // Trigger the event
    }
    public void RemoveItem(ItemsData item)
    {
        if (item.itemCategory == ItemCategory.zinc)
        {
            ZincInUse.Remove(item);
        }
        else if (item.itemCategory == ItemCategory.protein)
        {
            SkillsInInventory.Remove(item);
        }
        else
        {
            itemsInInventory.Remove(item);
        }
        OnItemsChanged?.Invoke(); // Trigger the event
    }

    public void CalculateStats()
    {

        GetComponent<PlayerStats>().playerStatsTotal.Reset();
        Debug.Log("Calculating stats");
        foreach (ItemsData item in itemsInInventory)
        {
            GetComponent<PlayerStats>().playerStatsTotal.Add(item);
        }

        foreach (ItemsData item in SkillsInInventory)
        {
            GetComponent<PlayerStats>().playerStatsTotal.Add(item);
        }

        foreach (ItemsData item in ZincInUse)
        {
            GetComponent<PlayerStats>().playerStatsTotal.Add(item);
        }
    }

}


