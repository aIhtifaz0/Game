using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private AstralModeManager AstralMode;

    [SerializeField] private GameObject Astral;
    [SerializeField] private GameObject Real;    

    public bool playerReady { get; set; }
    public bool astralReady { get; set; }
    
    private void OnTriggerStay2D(Collider2D collision){

        if(!playerReady){

            if(collision.TryGetComponent(out PlayerStateMachine player)){
                playerReady = true;
                Real.SetActive(false);
                Astral.SetActive(true);
            }
        }

        if(!astralReady){
            if(collision.TryGetComponent(out AstralStateMachine astral)){
                astralReady = true;
                Astral.SetActive(false);
                Real.SetActive(true);
            }
        }
    }

    private void Update(){

        if(playerReady && astralReady)
            SceneManager.LoadScene(SceneName);
    }
}
