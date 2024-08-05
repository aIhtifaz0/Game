using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour, Controls.IPlayerActions {

    private Controls controls;
    public Vector2 MovementValue { get; private set; }  

    #region Event
    public event Action JumpEvent;
    public event Action JumpUpEvent;
    public event Action DashEvent;
    public event Action InteractEvent;
    public event Action RealityEvent;
    public event Action MapEvent;
    public event Action PauseEvent;
    public event Action SwitchEvent;
    #endregion

    #region Boolean
    public bool IsAttacking;
    public bool IsInteract;
    public bool IsHeal;
    public bool IsMap;
    public bool IsPause;
    public bool IsSwitch;
    #endregion

    private void Start() {

        controls = new Controls();

        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    #region Input Callback
    public void OnMovement(InputAction.CallbackContext context) {
        
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {

        if (context.performed) {
            JumpEvent?.Invoke();
        }
        else if(context.canceled){
            JumpUpEvent?.Invoke();
        }
    }

    public void OnDash(InputAction.CallbackContext context) {

        if (!context.performed) { return; }
        DashEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context) {

        if (!context.performed) { return; }
        InteractEvent?.Invoke();
    }
    public void OnMap(InputAction.CallbackContext context) {

        if (!context.performed) { return; }
        MapEvent?.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context) {

        if (!context.performed) { return; }
        PauseEvent?.Invoke();
    }

    public void OnReality(InputAction.CallbackContext context){
        
        if (!context.performed) { return; }
        RealityEvent?.Invoke();
    }

    public void OnAstral(InputAction.CallbackContext context){

         if (!context.performed) { return; }
        SwitchEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context) {

        IsAttacking = false;
        if (context.performed) {
            IsAttacking = true;
        }
        else if (context.canceled) {
            IsAttacking = false;
        }
    }

    public void DisablingAttack() {

            IsAttacking = false;
    }

    public void DisablingInteract(){

            IsInteract = false;
            IsHeal = false;
            IsMap = false;
            IsPause = false;
            IsSwitch = false;
    }
    #endregion

    private void OnDestroy() {
        controls.Player.Disable();
    }
}
/*
    Input Reader handle all input system event for player control and all boolean to handle attack combo
    
*/