using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingManager : MonoBehaviour
{

    public static SettingManager instance;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Slider musicSlider;
    public Toggle muteToggle;
    public GameObject[] tabs;

    public SettingsData settingsData;

    [Header("Keybind Settings")]
    public GameObject keybindPrefab;
    // Start is called before the first frame update

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
    void Start()
    {
        LoadSettings();
        InitiateKeybinds();
        ShowResolutionDropdown();
        ShowQualityDropdown();

        SetTab(0);

        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(resolutionDropdown.value); });
        qualityDropdown.onValueChanged.AddListener(delegate { SetQuality(qualityDropdown.value); });
        fullScreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(fullScreenToggle.isOn); });

        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(volumeSlider.value); });
        musicSlider.onValueChanged.AddListener(delegate { OnVolumeChange(musicSlider.value); });
        //muteToggle.onValueChanged.AddListener(delegate { OnMute(muteToggle.isOn); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitiateKeybinds()
    {
        for (int i = 0; i < settingsData.keyBinds.Count; i++)
        {
            GameObject keybind = Instantiate(keybindPrefab, tabs[2].transform);
            settingsData.keyBinds[i].index = i;
            keybind.GetComponent<BindSetting>().index = i;
            keybind.GetComponentInChildren<TextMeshProUGUI>().text = settingsData.keyBinds[i].key;
            keybind.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = settingsData.keyBinds[i].actionName;
        }
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        }

        if (PlayerPrefs.HasKey("Quality"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
        }

        if (PlayerPrefs.HasKey("FullScreen"))
        {
            fullScreenToggle.isOn = (PlayerPrefs.GetInt("FullScreen") == 1 ? true : false);
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("Music");
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);
        PlayerPrefs.SetInt("FullScreen", (fullScreenToggle.isOn ? 1 : 0));
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        Debug.Log("Settings Saved");
        SoundManager.instance.SetVolume();

    }

    public void SetTab(int index)
    {
        switch (index)
        {
            case 0:
                tabs[0].SetActive(true);
                tabs[1].SetActive(false);
                tabs[2].SetActive(false);
                break;
            case 1:
                tabs[0].SetActive(false);
                tabs[1].SetActive(true);
                tabs[2].SetActive(false);
                break;
            case 2:
                tabs[0].SetActive(false);
                tabs[1].SetActive(false);
                tabs[2].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        SaveSettings();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        SaveSettings();
    }

    void ShowResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        Resolution currentResolution = Screen.currentResolution;

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution resolution = Screen.resolutions[i];
            string option = resolution.width + " x " + resolution.height + " @" + resolution.refreshRate + "Hz";
            options.Add(option);

            if (resolution.width == currentResolution.width && resolution.height == currentResolution.height && resolution.refreshRate == currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    // On resolutionDropdown value change
    public void OnResolutionChange(int resolutionIndex)
    {
        resolutionIndex = resolutionDropdown.value;
        Debug.Log("Resolution changed to: " + resolutionDropdown.options[resolutionDropdown.value].text);
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        SaveSettings();
    }

    public void OnVolumeChange(float volume)
    {
        volume = volumeSlider.value;
        //Search for the audio source
        SaveSettings();
    }

    public void OnMute(bool isMute)
    {
        if (isMute)
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
        SaveSettings();
    }


    void ShowQualityDropdown()
    {
        qualityDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            options.Add(QualitySettings.names[i]);

            if (QualitySettings.GetQualityLevel() == i)
            {
                qualityDropdown.value = i;
            }
        }

        qualityDropdown.AddOptions(options);
        qualityDropdown.RefreshShownValue();
    }


}
