using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UiManager : MonoBehaviour
{

    public static UiManager instance;
    private GameObject _player;
    public Slider healthBar;
    public Slider manaBar;

    [Header("Bar Color")]
    public Color flashColor;
    public Color normalColor;
    private int _playerHealth;
    private int _previousHealth;
    private int _previousMana;

    [Header("Inventory Items")]
    public GameObject inventoryPanel;
    public GameObject[] inventorySlots;
    public List<ItemsData> itemsInInventory = new List<ItemsData>();
    [Header("MiniMap")]
    public GameObject miniMapPosition;
    public GameObject miniMap;

    [Header("Skill Window")]
    public GameObject skillPrefab;

    [Header("Zinc Window")]
    public GameObject zincWindow;
    public ItemsData selectedItemsData;
    public Image zincIcon;
    public Sprite unSelectedZincIcon;
    public TMP_Text zincName;
    public TMP_Text zincDescription;
    public int zincCount = 0;
    public List<ItemsData> zincList = new List<ItemsData>();
    public List<GameObject> zincListGameObjects = new List<GameObject>();
    public TMP_Text nextLevelText;
    public TMP_Text nextLevelCost;
    public TMP_Text plasmaPointText;

    [Header("Pause")]
    public GameObject pauseMenu;

    void Awake()
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

    public Action OnZincChanged;
    public void DisplayZincWindow(ItemsData itemsData)
    {
        int zincLevel = itemsData.itemLevel;
        selectedItemsData = itemsData;
        zincIcon.sprite = itemsData.itemIcon;
        zincName.text = itemsData.itemName;
        zincDescription.text = itemsData.itemDescriptionLevel[zincLevel - 1];
        if (zincLevel < 3)
        {
            nextLevelText.text = itemsData.itemDescriptionLevel[zincLevel];
            nextLevelCost.text = itemsData.zincCost[zincLevel - 1].ToString();
        }
        else
        {
            nextLevelText.text = "Max Level";
            nextLevelCost.text = "-";
        }
    }

    public void UpdatePlasmaCount()
    {
        plasmaPointText.text = PlayerPrefs.GetInt("PlasmaPoint").ToString();
    }
    public void UpgradeZinc()
    {
        int plasmaPoint = PlayerPrefs.GetInt("PlasmaPoint");
        if (selectedItemsData.itemLevel < 4 && plasmaPoint >= selectedItemsData.zincCost[selectedItemsData.itemLevel - 1])
        {
            plasmaPoint -= selectedItemsData.zincCost[selectedItemsData.itemLevel - 1];
            PlayerPrefs.SetInt("PlasmaPoint", plasmaPoint);
            Debug.Log("Plasma Point: " + plasmaPoint);
            selectedItemsData.itemLevel++;
            zincCount++;
            zincName.text = selectedItemsData.itemName;
            zincDescription.text = selectedItemsData.itemDescriptionLevel[selectedItemsData.itemLevel - 1];
            if (selectedItemsData.itemLevel < 3)
            {
                nextLevelText.text = selectedItemsData.itemDescriptionLevel[selectedItemsData.itemLevel];
            }
            else
            {
                nextLevelText.text = "Max Level";
            }
            DisplayZincWindow(selectedItemsData);
            OnZincChanged?.Invoke();
        }
    }

    public void InitiateZincList()
    {
        
    }
    public void AddZincToList(ItemsData itemsData)
    {
        //if zinc already in list
        if (zincList.Contains(itemsData))
        {
            return;
        }
        zincList.Add(itemsData);
        OnZincChanged?.Invoke();
        
        //Set zinc data to gameobject
        foreach (var zincGameObject in zincListGameObjects)
        {
            if (zincGameObject.GetComponent<CardUI>().itemData == null)
            {
                zincGameObject.GetComponent<CardUI>().itemData = itemsData;
                zincGameObject.GetComponent<Image>().sprite = itemsData.itemActiveIcon;
                break;
            }
        }
    }

    public void UpdateZincList()
    {
        foreach (var zincGameObject in zincListGameObjects)
        {
            if (zincGameObject.GetComponent<CardUI>().itemData == null)
            {
                zincGameObject.GetComponent<Image>().sprite = unSelectedZincIcon;
            }
        }
    }
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        InitiateHealthBar();
        InitiateInventory();
        InitiateZincList();
        PlayerInventory.instance.OnItemsChanged += UpdateItemImages;
        _player.GetComponent<PlayerStats>().OnTakeDamage += UpdateHealthBar;
        _previousHealth = (int)_player.GetComponent<PlayerStats>().Health;
        _previousMana = (int)_player.GetComponent<PlayerStats>().mana;
        OnZincChanged += UpdateZincList;
        OnZincChanged += UpdatePlasmaCount;
        UpdateZincList();
        UpdatePlasmaCount();

    }

    // Update is called once per frame
    void Update()
    {
        miniMapPosition.transform.position = new Vector3(_player.transform.position.x, miniMapPosition.transform.position.y, 20);

        if (Input.GetKeyDown(KeyCode.M))
        {
            miniMap.SetActive(!miniMap.activeSelf);
        }

        // if key is up disable the mini map
        if (Input.GetKeyUp(KeyCode.M))
        {
            miniMap.SetActive(false);
        }

        UpdateHealthBar();
    }

    void InitiateHealthBar()
    {
        healthBar.maxValue = _player.GetComponent<PlayerStats>().MaxHealth;
        healthBar.value = _player.GetComponent<PlayerStats>().MaxHealth;
        healthBar.fillRect.GetComponent<Image>().color = normalColor;

        manaBar.maxValue = _player.GetComponent<PlayerStats>().MaxMana;
        manaBar.value = _player.GetComponent<PlayerStats>().MaxMana;
        manaBar.fillRect.GetComponent<Image>().color = normalColor;
    }

    void InitiateInventory()
    {
        UpdateItemImages();
    }

    public void InitiateSkillWindow()
    {
        GameObject skillWindow = GameObject.FindWithTag("SkillWindow");

        for (int i = 0; i < PlayerInventory.instance.SkillsInInventory.Count; i++)
        {
            GameObject skillCard = Instantiate(skillPrefab, skillWindow.transform);
            skillCard.GetComponent<ProteinCard>().item = PlayerInventory.instance.SkillsInInventory[i];
            skillCard.GetComponent<ProteinCard>().SetCard();
        }
    }

    void UpdateItemImages()
    {
        Debug.Log("Updating Item Images");
        itemsInInventory = PlayerInventory.instance.itemsInInventory;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < itemsInInventory.Count)
            {
                inventorySlots[i].SetActive(true);
                inventorySlots[i].GetComponent<Image>().sprite = itemsInInventory[i].itemIcon;
            }
            else
            {
                inventorySlots[i].SetActive(false);
            }
        }
    }

    void UpdateHealthBar()
    {
        int currentHealth = (int)_player.GetComponent<PlayerStats>().Health;
        int currentMana = (int)_player.GetComponent<PlayerStats>().mana;
        healthBar.value = currentHealth;
        manaBar.value = currentMana;
        StartCoroutine(FlashHealthBar());
        
        if (currentHealth < _previousHealth)
        {
            StartCoroutine(FlashHealthBar());
        }

        if (currentMana < _previousMana)
        {
            StartCoroutine(FlashHealthBar());
        }

        _previousHealth = currentHealth;
        _previousMana = currentMana;
    }

    IEnumerator FlashHealthBar()
    {
        int flashes = 5;
        for (int i = 0; i < flashes; i++)
        {
            healthBar.fillRect.GetComponent<Image>().color = flashColor;
            manaBar.fillRect.GetComponent<Image>().color = flashColor;
            yield return new WaitForSeconds(0.1f);
            healthBar.fillRect.GetComponent<Image>().color = normalColor;
            manaBar.fillRect.GetComponent<Image>().color = normalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Spawn()
    {
        SpawnDialogue("Press A to attack", 3, "A");
    }
    public void SpawnDialogue(string dialogue, float duration, string key = null)
    {
        var parent = GameObject.FindWithTag("UI");
        Vector3 spawnPosition = new Vector3(-655,-431,0);
        GameObject dialogueBox = Instantiate(Resources.Load("Prefabs/DialogBox") as GameObject, spawnPosition, Quaternion.identity, parent.transform);
        dialogueBox.transform.localPosition = spawnPosition;
        dialogueBox.GetComponent<DialogBox>().GetComponentInChildren<TMP_Text>().text = dialogue;
        dialogueBox.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Input/" + key);
        dialogueBox.GetComponent<DialogBox>().StartCoroutine(dialogueBox.GetComponent<DialogBox>().DestroyDialogBox(duration));
    }

    public void DamageUI(string damage)
    {
        var parentUI = GameObject.FindWithTag("UI");
        var playerLocation = _player.transform.position;
        Vector3 spawnPosition = new Vector3(playerLocation.x, playerLocation.y + 1, playerLocation.z);
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(spawnPosition);
        Vector2 screenPosition = new Vector2(
        ((viewportPosition.x * parentUI.GetComponent<RectTransform>().sizeDelta.x) - (parentUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f)),
        ((viewportPosition.y * parentUI.GetComponent<RectTransform>().sizeDelta.y) - (parentUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f))
        );
        GameObject damageText = Instantiate(Resources.Load("Prefabs/DamageText") as GameObject, screenPosition, Quaternion.identity, parentUI.transform);
        damageText.transform.localPosition = screenPosition;
        damageText.GetComponent<DamageText>().SetText(damage);
        damageText.GetComponent<DamageText>().StartCoroutine(damageText.GetComponent<DamageText>().FadeAndMoveUp(1));
    }

    public void ApplyZincSet()
    {
        PlayerInventory.instance.ZincInUse = zincList;
        zincWindow.SetActive(false);
    }

    public void OpenZincWindow()
    {
        zincWindow.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }  
}   

