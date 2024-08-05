using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI percentageCompleteText;

    [Header("Clear Data Button")]
    [SerializeField] private Button clearButton;

    public bool hasData { get; private set; } = false;

    private Button saveSlotButton;

    private void Awake() 
    {
        saveSlotButton = GetComponent<Button>();
        saveSlotButton.onClick.AddListener(OnSaveSlotButtonClicked);

        clearButton.onClick.AddListener(OnClearButtonClicked);
    }

    public void SetData(GameData data) 
    {
        // Tidak ada data untuk profileId ini
        if (data == null) 
        {
            hasData = false;
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            clearButton.gameObject.SetActive(false);
        }
        // Ada data untuk profileId ini
        else 
        {
            hasData = true;
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            clearButton.gameObject.SetActive(true);

           
        }
    }

    public string GetProfileId() 
    {
        return profileId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
        clearButton.interactable = interactable;
    }

    private void OnSaveSlotButtonClicked()
    {
        // Panggil event saat tombol "Save Slot" diklik
        SaveSlotsMenu saveSlotsMenu = FindObjectOfType<SaveSlotsMenu>();
        if (saveSlotsMenu != null)
        {
            saveSlotsMenu.OnSaveSlotClicked(this);
        }
    }

    private void OnClearButtonClicked()
    {
        // Panggil event saat tombol "Clear Data" diklik
        SaveSlotsMenu saveSlotsMenu = FindObjectOfType<SaveSlotsMenu>();
        if (saveSlotsMenu != null)
        {
            saveSlotsMenu.OnClearClicked(this);
        }
    }
}
