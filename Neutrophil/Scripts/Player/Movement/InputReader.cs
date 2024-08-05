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
    public event Action HealEvent;
    public event Action MapEvent;
    public event Action PauseEvent;
    #endregion

    #region Boolean
    public bool IsBasicAttacking;
    public bool IsAirAttacking;
    public bool IsSecondaryAttacking;
    public bool IsTertiaryAttacking;
    public bool IsUltimate;
    public bool IsInteract;
    public bool IsHeal;
    public bool IsMap;
    public bool IsPause;
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

    public void OnHeal(InputAction.CallbackContext context) {

        if (!context.performed) { return; }
        HealEvent?.Invoke();
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

    public void OnBasicAttack(InputAction.CallbackContext context) {

        IsBasicAttacking = false;
        if (context.performed) {
            IsBasicAttacking = true;
        }
        else if (context.canceled) {
            IsBasicAttacking = false;
        }

        IsAirAttacking = false;
        if (context.performed) {
            IsAirAttacking = true;
        }
        else if (context.canceled) {
            IsAirAttacking = false;
        }
    }

    public void OnSecondaryAttack(InputAction.CallbackContext context) {

        IsSecondaryAttacking = false;
        if (context.performed) {
            IsSecondaryAttacking = true;
        }
        else if (context.canceled) {
            IsSecondaryAttacking = false;
        }
    }

    public void OnTertiaryAttack(InputAction.CallbackContext context) {

        IsTertiaryAttacking = false;
        if (context.performed) {
            IsTertiaryAttacking = true;
        }
        else if (context.canceled) {
            IsTertiaryAttacking = false;
        }
    }

    public void OnUltimate(InputAction.CallbackContext context) {

        IsUltimate = false;
        if (context.performed) {
            IsUltimate = true;
        }
        else if (context.canceled) {
            IsUltimate = false;
        }
    }

    public void DisablingAttack() {

            IsBasicAttacking = false;
            IsAirAttacking = false;
            IsSecondaryAttacking = false;
            IsSecondaryAttacking = false;
            IsUltimate = false;
    }

    public void DisablingInteract(){

            IsInteract = false;
            IsHeal = false;
            IsMap = false;
            IsPause = false;
    }
    #endregion

    private void OnDestroy() {
        controls.Player.Disable();
    }

    public void Disable(){
        controls.Player.Disable();
    }

    public void Enable(){
        controls.Player.Enable();
    }
}
/*
    Input Reader handle all input system event for player control and all boolean to handle attack combo
    
*/