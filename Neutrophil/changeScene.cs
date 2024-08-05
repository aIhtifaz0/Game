using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public string GoToScene;
    public bool Auto = false;
    public float lifetime;
    public bool Reset = false;
    [field: SerializeField] public SavePos pos;

    private void Update()
    {
        /*if (Auto == true)
        {
            Invoke("AutoScene", lifetime);
        }*/
    }
    public void Change()
    {
        SceneManager.LoadScene(GoToScene);

        if(Reset == true)
        {
            pos.startpoint = new Vector3(51,74, 0);
        }
    }

    public void exit()
    {
        Application.Quit();
    }

    void AutoScene()
    {
        SceneManager.LoadScene(GoToScene);
    }
}
