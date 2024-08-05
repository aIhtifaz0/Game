using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtn : MonoBehaviour
{
    public GameObject setting;
    public GameObject Menu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openSetting()
    {
        setting.SetActive(true);
        Menu.SetActive(false);
    }

    public void CloseSetting()
    {
        setting.SetActive(false);
        Menu.SetActive(true);
    }
}
