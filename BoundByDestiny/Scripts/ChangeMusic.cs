using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public int indexMusic;
    private AudioSource loop;

    // Start is called before the first frame update
    void Start()
    {
        loop = GameObject.Find("Bgm").GetComponent<AudioSource>();

        if (GameObject.Find("Bgm") != null )
        {
            Music.Instance.ChangeMusic(indexMusic);
        }
        
      

        
    }

        

    // Update is called once per frame
    void Update()
    {
        
    }
}
