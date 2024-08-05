using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private bool isPlayerInside = false;
    public GameObject shopSystem; // Assume you have a ShopSystem class that manages the shop UI
    void Update()
    {
        // Check if the player is inside the trigger area and the E key is pressed
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            // Open the shop
            shopSystem.SetActive(true);
            //Get parant of shopSystem and get the ZincUI component
            ZincUI zincUI = shopSystem.GetComponentInParent<ZincUI>();
            //Call the OpenShop method
            zincUI.OpenShop();
        }

        if (isPlayerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            // Close the shop
            shopSystem.SetActive(false);
            
            ZincUI zincUI = shopSystem.GetComponentInParent<ZincUI>();
            //Call the CloseShop method
            zincUI.CloseShop();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            shopSystem.SetActive(false);
        }
    }
}