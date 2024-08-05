using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AreaManager : MonoBehaviour{

    public static AreaManager Instance { get; private set;}

    [field: SerializeField] public Light2D Light {get; private set; }
    [field: SerializeField] public GameObject IndorFish {get; private set; }

    public Transform[] SpawnPointRight;
    public Transform[] SpawnPointLeft;

    private float defIntensity = 1f;
    private float randomValue;
    public GameObject light;

    public AudioSource audio;
    public bool IsAnu;
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

    public void Special(){

        randomValue = UnityEngine.Random.Range(-1f, 1f);

        if(randomValue > 0f){
           var Fish = Instantiate(IndorFish, SpawnPointRight[UnityEngine.Random.Range(0, 2)].position, Quaternion.identity);
           Fish.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(randomValue < 0f){
            Instantiate(IndorFish, SpawnPointLeft[UnityEngine.Random.Range(0, 2)].position, Quaternion.identity);
        }
    }

    public void Ultimate(){

        //Light.intensity = 0.03f;
        var anim = light.GetComponent<Animator>();
        anim.SetTrigger("Ultimate");

    }

    public void Normal(){

        Light.intensity = defIntensity;
    }

    public void StartMusic()
    {
        audio.Play();
    }

    private void Update()
    {
        if(IsAnu)
        {
            StartMusic();
            IsAnu = false;
        }
    }
}
