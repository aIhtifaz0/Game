using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracableManager : MonoBehaviour
{
    public GameObject Gate1;
    public GameObject Gate2;
    public GameObject GateBoss;
    public GameObject GateAstral1;
    public Cinemachine.CinemachineVirtualCamera GateCam;
    public GameObject Camera;

    public GameObject panel;
    private Animator PanelAnim;

    void Start()
    {
        GateCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        PanelAnim = panel.GetComponent<Animator>();
    }

    public void OpenGate1()
    {
        PanelAnim.SetTrigger("End");
        Invoke("Gate1Start", 1f);
        Invoke("Open", 1.7f);
        Invoke("Gate1End", 3.5f);

    }

    public void OpenGate2()
    {
        PanelAnim.SetTrigger("End");
        Invoke("Gate2Start", 1f);
        Invoke("Open2", 1.7f);
        Invoke("Gate2End", 3.5f);

    }

    public void OpenGateAstral()
    {
        PanelAnim.SetTrigger("End");
        Invoke("GateAstralStart", 1f);
        Invoke("Open3", 1.7f);
        Invoke("GateAstralEnd", 3.5f);

    }

    public void OpenGateBoss()
    {
        PanelAnim.SetTrigger("End");
        Invoke("GateBossStart", 1f);
        Invoke("Open4", 1.7f);
        Invoke("GateBossEnd", 3.5f);

    }

    void Gate1Start()
    {
        //GateCam.Priority = 15;
        var CamAnim = Camera.GetComponent<Animator>();
        CamAnim.SetTrigger("Gate1");
        
    }

    void Gate1End()
    {
        //GateCam.Priority = 0;
        PanelAnim.SetTrigger("End");

        //Invoke("delay", 1.5f);
        delay();
    }

    void Gate2Start()
    {
        //GateCam.Priority = 15;
        var CamAnim = Camera.GetComponent<Animator>();
        CamAnim.SetTrigger("Gate2");
    }

    void Gate2End()
    {
        //GateCam.Priority = 0;
        PanelAnim.SetTrigger("End");

        //Invoke("delay", 1.5f);
        delay();
    }

    void GateAstralStart()
    {
        //GateCam.Priority = 15;
        var CamAnim = Camera.GetComponent<Animator>();
        CamAnim.SetTrigger("Gate3");
    }

    void GateAstralEnd()
    {
        //GateCam.Priority = 0;
        PanelAnim.SetTrigger("End");

        //Invoke("delay", 1.5f);
        delay();
    }

    void GateBossStart()
    {
        //GateCam.Priority = 15;
        var CamAnim = Camera.GetComponent<Animator>();
        CamAnim.SetTrigger("Gate4");
    }

    void GateBossEnd()
    {
        //GateCam.Priority = 0;
        PanelAnim.SetTrigger("End");

        //Invoke("delay", 1.5f);
        delay();
    }
    
    void delay()
    {
        var CamAnim = Camera.GetComponent<Animator>();
        CamAnim.SetTrigger("Back");
    }

    void delay2()
    {
        var CamAnim = Camera.GetComponent<Animator>();
        CamAnim.SetTrigger("Back");
    }

    void delay3()
    {
        var CamAnim = Camera.GetComponent<Animator>();
        CamAnim.SetTrigger("Back");
    }

    void delay4()
    {
        var CamAnim = Camera.GetComponent<Animator>();
        CamAnim.SetTrigger("Back");
    }

    void Open()
    {
        var anim = Gate1.GetComponent<Animator>();
        anim.SetTrigger("Open");
    }

    void Open2()
    {
        var anim = Gate2.GetComponent<Animator>();
        anim.SetTrigger("Open");
    }

    void Open3()
    {
        var anim = GateAstral1.GetComponent<Animator>();
        anim.SetTrigger("Open");
    }

    void Open4()
    {
        var anim = GateBoss.GetComponent<Animator>();
        anim.SetTrigger("Open");
    }
}
