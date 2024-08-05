using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveObject : MonoBehaviour
{
    public GameObject PauseParent;
    public bool IsOn = false;
    public GameObject settingObj;
    public GameObject panel;
    public string MenuScene;
    // Update is called once per frame
    void Update()
    {
        if(IsOn == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseParent.SetActive(true);
                Invoke("delay", 0.05f);
                
            }
        }

        if(IsOn == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseParent.SetActive(false);
                Time.timeScale = 1;
                IsOn = false;
            }
        }
    }

    void delay()
    {
        IsOn = true;
        Time.timeScale = 0;
    }

    public void resume()
    {
        PauseParent.SetActive(false);
        Time.timeScale = 1;
        IsOn = false;
    }

    public void Opensetting()
    {
        gameObject.SetActive(false);
        settingObj.SetActive(true);
    }

    public void closeSetting()
    {
        gameObject.SetActive(true);
        settingObj.SetActive(false);
    }

    public void exit()
    {
        Time.timeScale = 2;
        var anim = panel.GetComponent<Animator>();
        anim.SetTrigger("End");
        Invoke("delayexit", 1f);
        
    }

    void delayexit()
    {
        PlayerPrefs.SetInt("loadSaved", 1);
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        SceneManager.LoadScene(MenuScene);
    }
}
