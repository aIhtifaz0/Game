using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    public GameObject boss;
    public GameObject panel;
    public string scene;
    public float delaytime;
    void Update()
    {
        if(boss == null)
        {
            var anim = panel.GetComponent<Animator>();
            anim.SetTrigger("End");
            Invoke("delayScene", delaytime);
        }
    }

    void delayScene()
    {
        SceneManager.LoadScene(scene);
    }
}
