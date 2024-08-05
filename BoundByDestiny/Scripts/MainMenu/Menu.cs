using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("First Selected Button")]
    [SerializeField] private Button firstSelected;

    protected virtual void OnEnable() 
    {
        SetFirstSelected(firstSelected);
    }

    public void SetFirstSelected(Button firstSelectedButton) 
    {
        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
        else
        {
            Debug.LogWarning("First selected button is not assigned.");
        }
    }
}
