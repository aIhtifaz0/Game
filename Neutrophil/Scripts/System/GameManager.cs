using UnityEngine;

public class GameManager : MonoBehaviour{

    bool isPause;
    public GameObject BindingMenu;

    void Start(){
        
        isPause = false;
    }

    public void OnTogglePause(){

        isPause = !isPause;

        if(isPause){

            Time.timeScale = 0;
            BindingMenu.SetActive(true);
        }
        else{

            Time.timeScale = 1;
            BindingMenu.SetActive(false);
        }
    }

    public void UnPause(){

        isPause = !isPause;
        Time.timeScale = 1;
        BindingMenu.SetActive(false);
    }
}
