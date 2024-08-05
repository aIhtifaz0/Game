using UnityEngine;

public class MeleeWeaponHandler : MonoBehaviour{

    [SerializeField] GameObject WeaponLogic;
    
    public void EnableWeapon(){
        WeaponLogic.SetActive(true);
    }

    public void DisableWeapon(){
        WeaponLogic.SetActive(false);
    }
}
