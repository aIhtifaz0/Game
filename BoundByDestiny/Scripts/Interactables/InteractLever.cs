using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLever : MonoBehaviour
{
    public enum gateName {gate1, gate2, gateAstral, gateboss};

    [field: SerializeField] public gateName Name { get; private set; }

    public InteracableManager Interact;
    private bool alreadyPress;

    private bool Gate1, Gate2, GateAstral, GateBoss;

    // Start is called before the first frame update
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {   

            if(!alreadyPress){

                if(Input.GetKeyDown(KeyCode.F)){

                    alreadyPress = true;

                    if(!Gate1){
                        if(Name == gateName.gate1)
                            Interact.OpenGate1();
                            Gate1 = true;
                    }
                    
                    if(!Gate2){
                        if(Name == gateName.gate2)
                            Interact.OpenGate2();
                            Gate2 = true;
                    }
                    
                    if(!GateAstral){
                        if(Name == gateName.gateAstral)
                            Interact.OpenGateAstral();
                            GateAstral = true;
                    }
                    
                    if(!GateBoss){
                        if(Name == gateName.gateboss)
                            Interact.OpenGateBoss();
                            GateBoss = true;
                    }
                }
            }
        }
    }
}
