using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioSource player;
    public AudioSource npc;

    [Header("SFX")]
    public AudioClip[] sfxClips;

    [Header("Music")]
    public AudioClip[] musicClips;

    [Header("Player")]
    public AudioClip[] playerClips;

    [Header("NPC")]
    public AudioClip[] npcClips;

    [Header("Music Audio Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip inGameMusic;
    public AudioClip bossMusic;

    private void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        if(sfxSource != null)
            sfxSource.volume = PlayerPrefs.GetFloat("Volume", 1);
        if(musicSource != null)
            musicSource.volume = PlayerPrefs.GetFloat("Music", 1);
        if(player != null)
            player.volume = PlayerPrefs.GetFloat("Volume", 1);
        if(npc != null)
            npc.volume = PlayerPrefs.GetFloat("Volume", 1);

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            PlayMainMusic();
        }
        else if(SceneManager.GetActiveScene().name == "Boss")
        {
            PlayBossMusic();
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            PlayInGameMusic();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVolume()
    {
        //check is mute
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            sfxSource.volume = 0;
            musicSource.volume = 0;
            player.volume = 0;
            npc.volume = 0;
        }
        else
        {
            sfxSource.volume = PlayerPrefs.GetFloat("Volume");
            musicSource.volume = PlayerPrefs.GetFloat("Music");
            player.volume = PlayerPrefs.GetFloat("Volume");
            npc.volume = PlayerPrefs.GetFloat("Volume");
        }

        Debug.Log("Volume: " + PlayerPrefs.GetFloat("Volume"));
        Debug.Log("Music: " + PlayerPrefs.GetFloat("Music"));
        
    }
    public void PlayDialogue(AudioClip clips)
    {
        npc.clip = null;
        npc.clip = clips;
        npc.Play();
    }

    public void PlayMainMusic()
    {
        musicSource.clip = mainMenuMusic;
        musicSource.Play();
    }

    public void PlayInGameMusic()
    {
        musicSource.clip = inGameMusic;
        musicSource.Play();
    }

    public void PlayBossMusic()
    {
        musicSource.clip = bossMusic;
        musicSource.Play();
    }
}
