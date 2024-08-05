using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;

public class ShopUI : MonoBehaviour
{

    public bool isShop;
    public static ShopUI instance;
    public List<ItemsData> avaliableItems = new List<ItemsData>();
    public TMP_Text goldText;

    [Header("Shop")]
    public GameObject cardPrefab;
    public Transform cardParentAttack;
    public Transform cardParentHealth;
    public Transform cardParentDefense;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    List<ItemsData> itemOffensive = new List<ItemsData>();
    List<ItemsData> itemDefensive = new List<ItemsData>();
    List<ItemsData> itemConsumable = new List<ItemsData>();
    void Start()
    {
        UpdateText();
        OnBuyItem += UpdateText;

        Debug.Log("Avaliable Items: " + avaliableItems.Count);
        foreach (ItemsData item in avaliableItems)
        {
            Debug.Log("Item Type: " + item.itemType.ToString());
            switch (item.itemType)
            {
                case ItemType.Offensive:
                    itemOffensive.Add(item);
                    break;
                case ItemType.Defensive:
                    itemDefensive.Add(item);
                    break;
                case ItemType.caster:
                    itemConsumable.Add(item);
                    break;
                default:
                    break;
            }
        }

        if (isShop)
        {
            //Instantiate card and set data from itemOffensive, itemDefensive, itemConsumable
            for (int i = 0; i < itemOffensive.Count; i++)
            {
                GameObject card = Instantiate(cardPrefab, cardParentAttack);
                CardUI cardUI = card.GetComponent<CardUI>();
                cardUI.itemData = itemOffensive[i];
                cardUI.SetCard();
            }

            for (int i = 0; i < itemDefensive.Count; i++)
            {
                GameObject card = Instantiate(cardPrefab, cardParentDefense);
                CardUI cardUI = card.GetComponent<CardUI>();
                cardUI.itemData = itemDefensive[i];
                cardUI.SetCard();
            }

            for (int i = 0; i < itemConsumable.Count; i++)
            {
                GameObject card = Instantiate(cardPrefab, cardParentHealth);
                CardUI cardUI = card.GetComponent<CardUI>();
                cardUI.itemData = itemConsumable[i];
                cardUI.SetCard();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateText()
    {
        Debug.Log("Update Text");
        int gold = PlayerInventory.instance.Gold;
        goldText.text = gold.ToString();
    }

    public Action OnBuyItem;

}
