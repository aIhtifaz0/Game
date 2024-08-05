using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "SettingsData")]
public class SettingsData : ScriptableObject
{
    public int resolutionIndex;
    public bool isFullScreen;
    public int qualityIndex;
    public float volume;

    public List<KeyBind> keyBinds = new List<KeyBind>();
}

[System.Serializable]
//Keybind
public class KeyBind
{
    public int index;
    public string actionName;
    public string key;
    public KeyCode keyBind;
    public KeyBind(string k, KeyCode kb)
    {
        key = k;
        keyBind = kb;
    }
}