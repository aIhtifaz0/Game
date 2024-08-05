using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [field: SerializeField] public AudioSource SoundFx { get; private set; }

    public AudioClip Walk;
    public AudioClip Attack;
    public AudioClip Dash;
    public AudioClip Death;
    public AudioClip Teleport;
    public AudioClip Hit;
    public AudioClip Jump;
    public AudioClip Shoot;

    private void Awake(){

        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void PlaySound(AudioClip clip){

        SoundFx.PlayOneShot(clip);
    }
}


