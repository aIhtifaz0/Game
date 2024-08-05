    using UnityEngine;

public class MeleeFallState : MeleeBaseState{
    public MeleeFallState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    private readonly int FallHash = Animator.StringToHash("Fall");
    private const float CrossFadeDuration = 0.1f;


    public override void Enter() {

        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        #region Collision Check
        if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer)){ //checks if set box overlaps with ground >> && stateMachine.IsJumping bug IsGrounded cannot true
           
            stateMachine.IsGrounded = true;
        }
        #endregion

        if (stateMachine.IsGrounded == true) { // || !stateMachine.IsJumping BasicAttack after air attack bug

            stateMachine.SwitchState(new MeleeSpotState(stateMachine));
        }
    }

    public override void Exit() { 

        stateMachine.IsGrounded = false;
    }
}

/*
     getting animation from animator
     Play animation with crossfade for smooth transition

     Reset lastongroundtime for player ready to jump after resetting
     getting input check for air moving
     getting input check for attack and if attack switch to any attack state based on input
     check player facing based on x input
     Airborne : checking if player dashing or just moving with move method
     Move : Player move with addforce based on player speed and input, and added speed acceleration
     added jump/fall acceleration based on player y velocity
*/
