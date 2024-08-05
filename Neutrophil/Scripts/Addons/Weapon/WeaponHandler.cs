using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject WeaponLogic;
    [SerializeField] GameObject NeutroBladeLogic;
    [SerializeField] GameObject NeutroBladeUltimateLogic;
    [SerializeField] GameObject PaghoSpearLogic;
    [SerializeField] GameObject PaghoSpearUltimateLogic;
    [SerializeField] GameObject NeutroSpikeLogic;
    [SerializeField] GameObject NeutroSpikeUltimateLogic;

    public void EnableWeapon(){
        WeaponLogic.SetActive(true);
    }

    public void DisableWeapon(){
        WeaponLogic.SetActive(false);
    }

    public void EnableNeutroBladeWeapon(){
        NeutroBladeLogic.SetActive(true);
    }

    public void DisableNeutroBladeWeapon(){
        NeutroBladeLogic.SetActive(false);
    }

    public void EnableNeutroBladeUltimate(){
        NeutroBladeUltimateLogic.SetActive(true);
    }

    public void DisableNeutroBladeUltimate(){
        NeutroBladeUltimateLogic.SetActive(false);
    }

    public void EnablePaghoSpearWeapon(){
        PaghoSpearLogic.SetActive(true);
    }

    public void DisablePaghoSpearWeapon(){
        PaghoSpearLogic.SetActive(false);
    }

    public void EnablePaghoSpearUltimate(){
        PaghoSpearUltimateLogic.SetActive(true);
    }

    public void DisablePaghoSpearUltimate(){
        PaghoSpearUltimateLogic.SetActive(false);
    }

    public void EnableNeutroSpikeWeapon(){
        NeutroSpikeLogic.SetActive(true);
    }

    public void DisableNeutroSpikeWeapon(){
        NeutroSpikeLogic.SetActive(false);
    }

    public void EnableNeutroSpikeUltimate(){
        NeutroSpikeUltimateLogic.SetActive(true);
    }

    public void DisableNeutroSpikeUltimate(){
        NeutroSpikeUltimateLogic.SetActive(false);
    }
}
