using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    
    public static Music Instance { get; set; }

    public AudioClip[] Clips;
    public AudioSource Source;
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void ChangeMusic(int indexMusic)
    {
        if(Source.clip != Clips[indexMusic])
        {
            Source.Stop();
            Source.clip = Clips[indexMusic];
            Source.Play();

        }
    }

    // Update is called once per frame
    
}
