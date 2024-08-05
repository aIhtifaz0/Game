using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClose : MonoBehaviour
{
    public GameObject gate;
    public GameObject Bossmusic;
    public GameObject finish;
    public Animator GateAnim;
    //public AudioSource audio;
    public GameObject bgm;
    public bool IsEnter = false;
    public bool IsTrigger;

   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && IsEnter == false)
        {
            Time.timeScale = 1;
            

            //Instantiate(bgm, transform.position, Quaternion.identity);
            //audio.Play();
            //AreaManager.Instance.IsAnu = true;
            IsEnter = true;
            Invoke("delay", 0.3f);
        }
    }

    private void Update()
    {
        if (IsTrigger)
        {
            finish.SetActive(true);
            Bossmusic.SetActive(true);
            GateAnim.SetTrigger("Close");
            //Music.Instance.ChangeMusic(2);
            IsTrigger = false;
        }
    }

    void delay()
    {
        IsTrigger = true;
    }

}
