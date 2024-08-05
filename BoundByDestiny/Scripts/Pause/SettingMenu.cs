using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer mainAudioMixer; // Reference to the AudioMixer
    public Slider mastervol, musicvol, sfxvol; // Reference to the sliders
    public GameObject mainMenu; // Reference to the MainMenu UI

    void Start()
    {
        // Initialize sliders to match current AudioMixer settings
        float value;
        if (mainAudioMixer.GetFloat("Mastervol", out value))
            mastervol.value = value;
        if (mainAudioMixer.GetFloat("Musicvol", out value))
            musicvol.value = value;
        if (mainAudioMixer.GetFloat("Sfxvol", out value))
            sfxvol.value = value;
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("Mastervol", mastervol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("Musicvol", musicvol.value);
    }

    public void ChangeSfxVolume()
    {
        mainAudioMixer.SetFloat("Sfxvol", sfxvol.value);
    }

    public void OnBackClicked()
    {
        gameObject.SetActive(false); // Disable the SettingMenu UI
        mainMenu.SetActive(true); // Activate the MainMenu UI
    }
}
