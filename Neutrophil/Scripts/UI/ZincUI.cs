using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ZincUI : MonoBehaviour
{

    public List<GameObject> itemShopList = new List<GameObject>();

    public ItemsData selectedItemsData;
    public Image displayImage;
    public TMP_Text displayText;
    public TMP_Text displayDescription;
    public TMP_Text displayPrice;
    
    public TMP_Text glycoCoinText;
    public TMP_Text plasmaPointText;

    public Action onZincChanged;
    // Start is called before the first frame update
    void Start()
    {
        glycoCoinText.text = PlayerPrefs.GetInt("GlycoCoin").ToString();
        plasmaPointText.text = PlayerPrefs.GetInt("PlasmaPoint").ToString();
        onZincChanged += UpdateDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void UpdateDisplay()
    {
        SetDisplay(selectedItemsData);
    }
    public void SetDisplay(ItemsData itemData)
    {
        if (itemData.isItemBought == true)
        {
            displayImage.sprite = itemData.itemActiveIcon;
        }
        else
        {
            displayImage.sprite = itemData.itemIcon;
        }
        displayText.text = itemData.itemName;
        displayDescription.text = itemData.itemDescription;
        displayPrice.text = itemData.itemPrice.ToString();
    }

    public void BuyItem()
    {
        ItemsData itemData = selectedItemsData;
        if (itemData.itemPrice > PlayerPrefs.GetInt("GlycoCoin"))
        {
            Debug.Log("Not enough GlycoCoin");
            return;
        }
        PlayerPrefs.SetInt("GlycoCoin", PlayerPrefs.GetInt("GlycoCoin") - itemData.itemPrice);
        PlayerInventory.instance.AddItem(itemData);
        glycoCoinText.text = PlayerPrefs.GetInt("GlycoCoin").ToString();
        itemData.isItemBought = true;
        onZincChanged?.Invoke();
    }

    public void OpenShop()
    {
        //Freeze the game
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        //disable first child
        GameObject firstChild = transform.GetChild(0).gameObject;
        firstChild.SetActive(false);
        //Unfreeze the game
        Time.timeScale = 1;
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}
