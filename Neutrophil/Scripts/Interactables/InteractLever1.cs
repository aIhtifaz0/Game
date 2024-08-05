using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLever : MonoBehaviour
{
    public enum gateName {gate1, gate2, gateAstral, gateboss};

    [field: SerializeField] public gateName Name { get; private set; }

    [SerializeField] private GameObject InteractInfo;
    public InteracableManager Interact;
    private bool alreadyPress;
    private Animator anim;
    private bool Gate1, Gate2, GateAstral, GateBoss;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {   

            if(!alreadyPress){
                InteractInfo.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F)){

                    alreadyPress = true;

                    if(!Gate1){
                        if(Name == gateName.gate1)
                            Interact.OpenGate1();
                            InteractInfo.SetActive(false);
                            Gate1 = true;
                        anim.SetBool("IsOn", true);
                    }
                    
                    if(!Gate2){
                        if(Name == gateName.gate2)
                            Interact.OpenGate2();
                             InteractInfo.SetActive(false);
                            Gate2 = true;
                        anim.SetBool("IsOn", true);
                    }
                    
                    if(!GateAstral){
                        if(Name == gateName.gateAstral)
                            Interact.OpenGateAstral();
                            GateAstral = true;
                        InteractInfo.SetActive(false);
                        anim.SetBool("IsOn", true);
                    }
                    
                    if(!GateBoss){
                        if(Name == gateName.gateboss)
                            Interact.OpenGateBoss();
                            GateBoss = true;
                        InteractInfo.SetActive(false);
                        anim.SetBool("IsOn", true);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            alreadyPress = false;
            
            InteractInfo.SetActive(false);
        }
    }
}
