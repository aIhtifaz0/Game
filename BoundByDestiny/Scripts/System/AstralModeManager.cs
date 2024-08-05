using UnityEngine;

public class AstralModeManager : MonoBehaviour{

    [SerializeField] private GameObject Real;
    [SerializeField] private GameObject Astral;
    
    public bool IsAstral = false; 
    
    public void ModeChange(){

        if(!IsAstral){
            
            IsAstral = true;
            Real.SetActive(false);
            Astral.SetActive(true);    
        }
        
    }

    public void ModeRevert(){

        if(IsAstral){

            IsAstral = false;
            Real.SetActive(true);
            Astral.SetActive(false);
        }
    }

    private void Update(){

        Debug.Log(Time.timeScale);
    }
}
