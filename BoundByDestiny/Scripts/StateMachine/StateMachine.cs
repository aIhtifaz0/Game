using UnityEngine;

public abstract class StateMachine : MonoBehaviour{

    private State currentState;

    private void Update(){
        
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState){
        
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}

/*
    Exiting last state and assign current one
    and call the enter state

    calling tick state everytime
*/