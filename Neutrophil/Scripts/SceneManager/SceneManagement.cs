using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public GameObject[] Doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoTo(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    
}
