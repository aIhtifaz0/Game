using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BindSetting : MonoBehaviour
{
    private SettingsData settingsData;
    public Button changeKeybindButton;
    public TMP_Text keybindText;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        settingsData = SettingManager.instance.settingsData;
        changeKeybindButton.onClick.AddListener(ChangeKeybind);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeKeybind()
    {
        StartCoroutine(WaitForKey(index));
    }

    IEnumerator WaitForKey(int keyindex)
    {
        keybindText.text = "Press a key";
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(keyCode))
            {
                // Assuming settingsData.keyBinds[i] is the keybind you want to change
                settingsData.keyBinds[keyindex].key = keyCode.ToString();
                settingsData.keyBinds[keyindex].keyBind = keyCode;
                keybindText.text = keyCode.ToString();
                break;
            }
        }
    }
}
