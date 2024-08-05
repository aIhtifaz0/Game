using UnityEngine;

public class FlyWeaponHandler : MonoBehaviour{
    
    [SerializeField] GameObject WeaponLogic;
    
    public void EnableWeapon(){
        WeaponLogic.SetActive(true);
    }

    public void DisableWeapon(){
        WeaponLogic.SetActive(false);
    }
}
