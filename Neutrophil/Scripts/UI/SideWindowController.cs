using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SideWindowController : MonoBehaviour
{

    public GameObject sideWindow;
    public GameObject[] windows;
    public int currentWindow = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (currentWindow == 4)
            {
                UpdateWindow();
                SetWindow(0);
            }
            else
            {
                CloseWindow();
                UpdateWindow();
            }
        }
    }

    public void SetWindow(int windowIndex)
    {
        if (windowIndex == currentWindow)
        {
            return;
        }
        currentWindow = windowIndex;
        UpdateWindow();
    }
    void UpdateWindow()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == currentWindow)
            {
                windows[i].SetActive(true);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }

        if (currentWindow == 0)
        {
            InitiateSkillWindow();
        }
        else
        {
            //destroy all children of window 0
            foreach (Transform child in windows[0].transform)
            {
                foreach (Transform grandChild in child)
                {
                    Destroy(grandChild.gameObject);
                }
            }

        }
    }


    void InitiateSkillWindow()
    {
        UiManager.instance.InitiateSkillWindow();

    }

    void CloseWindow()
    {
        windows[currentWindow].SetActive(false);
        currentWindow = 4;
    }
}
