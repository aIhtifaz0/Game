using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomScript : MonoBehaviour
{
    public GameObject secretBound;
    public GameObject CameraBound;
    Cinemachine.CinemachineConfiner2D confiner;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator anim = gameObject.GetComponent<Animator>();

            anim.SetTrigger("anu");

            

            secretBound.GetComponent<SecretBound>().EnablePol();
            CameraBound.GetComponent<Cinemachine.CinemachineConfiner2D>().InvalidateCache();
            
        }
        
    }
}
