using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private string SceneName;
    
    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.TryGetComponent(out PlayerStateMachine player)){
            SceneManager.LoadScene(SceneName);
        }
        
    }
}
