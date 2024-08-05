using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour {

    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera cineCam;
    private float timer;
    private float timerTotal;
    private float StartingIntensity;

    private void Awake(){

        Instance = this;
        cineCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float intensity, float time){

        CinemachineBasicMultiChannelPerlin multiChanelPerlin = 
            cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        multiChanelPerlin.m_AmplitudeGain = intensity;

        StartingIntensity = intensity;
        timerTotal = time;
        timer = time;
    }

    public void Update(){

        if(timer > 0f){
            timer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin multiChanelPerlin = 
                cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            multiChanelPerlin.m_AmplitudeGain = Mathf.Lerp(StartingIntensity, 0f,(1 - ( timer / timerTotal)));
        }
    }
}
