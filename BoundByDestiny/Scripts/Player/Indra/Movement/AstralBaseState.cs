using UnityEngine;

public abstract class AstralBaseState : State{

    //only class inherit can access it
    protected AstralStateMachine stateMachine;

    public AstralBaseState(AstralStateMachine stateMachine){

        this.stateMachine = stateMachine; 
    }

    #region Check Methods
    
    protected bool CanJump() {
        return stateMachine.LastOnGroundTime > 0 && !stateMachine.IsJumping;
    }

    protected bool CanDash() {

            if (!stateMachine.IsDashing && stateMachine._dashesLeft < stateMachine.ControllerData.dashAmount && !stateMachine._dashRefilling) {

                stateMachine.StartCoroutine(stateMachine.RefillDash(1));
            }
            return stateMachine._dashesLeft > 0;
    }

    protected void CheckFacing() {

        if (stateMachine._moveInput.x > 0 && !stateMachine.IsFacingRight) {
            Turn();
        }
        else if (stateMachine._moveInput.x < 0 && stateMachine.IsFacingRight) {
            Turn();
        }
    }
    #endregion

    protected void Turn() {

        stateMachine.IsFacingRight = !stateMachine.IsFacingRight;

        Vector3 scale = stateMachine.transform.localScale;
        scale.x *= -1;
        stateMachine.transform.localScale = scale;

    }

    protected void Destroy(GameObject gameObject, float timer){

        Destroy(gameObject, timer);
    }
}

/*
    CheckDirectionToFace :
    Checking Player direction based on transform y rotation and call Turn method when true

    CanJump :
    CanJump true when player on the air & not jumping

    CanDash :
    dash will refill when player not dashing, have dash amount left, & not refilling dash
    CanDash will true if player have dashes amount
    
    Turn :
    Change the x local scale to minus 1 based on IsFacingRight boolean
*/
