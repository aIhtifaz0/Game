using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject weaponLogic;
    [SerializeField] GameObject weaponUpLogic;

    public void EnableWeapon(){
        weaponLogic.SetActive(true);
    }

    public void DisableWeapon(){
        weaponLogic.SetActive(false);
    }

    public void EnableUpWeapon(){
        weaponUpLogic.SetActive(true);
    }

    public void DisableUpWeapon(){
        weaponUpLogic.SetActive(false);
    }
}
