using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject[] panels;

    public void OpenPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == index)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void ClosePanel(int index)
    {
        panels[0].SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit(){

        Application.Quit();
    }
}
