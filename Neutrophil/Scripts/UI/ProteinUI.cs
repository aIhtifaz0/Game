using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ProteinUI : MonoBehaviour
{
    public Action OnWindowChanged;
    private int _selectedWindow = 0;
    [Header("Protein")]
    
    public Transform proteinOffensive;
    public Transform proteinDefensive;
    public Transform proteinCaster;
    // Start is called before the first frame update
    void Start()
    {
        //Get CardUi in each parent
        CardUI[] cardUIsAttack = proteinOffensive.GetComponentsInChildren<CardUI>();
        CardUI[] cardUIsHealth = proteinDefensive.GetComponentsInChildren<CardUI>();
        CardUI[] cardUIsDefense = proteinCaster.GetComponentsInChildren<CardUI>();

        //Set card data
        for (int i = 0; i < cardUIsAttack.Length; i++)
        {
            cardUIsAttack[i].SetCard();
        }
        for (int i = 0; i < cardUIsHealth.Length; i++)
        {
            cardUIsHealth[i].SetCard();
        }
        for (int i = 0; i < cardUIsDefense.Length; i++)
        {
            cardUIsDefense[i].SetCard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeWindow(int window)
    {
        _selectedWindow = window;
        switch (window)
        {
            case 0:
                proteinOffensive.gameObject.SetActive(true);
                proteinDefensive.gameObject.SetActive(false);
                proteinCaster.gameObject.SetActive(false);
                break;
            case 1:
                proteinOffensive.gameObject.SetActive(false);
                proteinDefensive.gameObject.SetActive(true);
                proteinCaster.gameObject.SetActive(false);
                break;
            case 2:
                proteinOffensive.gameObject.SetActive(false);
                proteinDefensive.gameObject.SetActive(false);
                proteinCaster.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        OnWindowChanged?.Invoke();
    }
}
