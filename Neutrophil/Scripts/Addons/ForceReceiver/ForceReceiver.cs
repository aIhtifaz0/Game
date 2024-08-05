using UnityEngine;
using UnityEngine.AI;


public class ForceReceiver : MonoBehaviour{

    [SerializeField] public PlayerStateMachine stateMachine;
    [SerializeField] private float drag = 0.3f;

    private Vector3 impact;
    private Vector3 dampingVelocity;

    public Vector2 Movement => impact;

    private void Update(){

        if (!stateMachine._isDashAttacking) {

            if (stateMachine.Rigidbody.velocity.y < 0 && stateMachine._moveInput.y < 0) {
                SetGravityScale(stateMachine.ControllerData.GravityScale * stateMachine.ControllerData.FastFallGravityMult);
                stateMachine.Rigidbody.velocity = new Vector2(stateMachine.Rigidbody.velocity.x, Mathf.Max(stateMachine.Rigidbody.velocity.y, -stateMachine.ControllerData.MaxFastFallSpeed));
            }
            else if ((stateMachine.IsJumping || stateMachine._isJumpFalling) && Mathf.Abs(stateMachine.Rigidbody.velocity.y) < stateMachine.ControllerData.JumpHangTimeThreshold) {
                SetGravityScale(stateMachine.ControllerData.GravityScale * stateMachine.ControllerData.jumpHangGravityMult);
            }
            else if (stateMachine.Rigidbody.velocity.y < 0) {
                SetGravityScale(stateMachine.ControllerData.GravityScale * stateMachine.ControllerData.FallGravityMult);
                stateMachine.Rigidbody.velocity = new Vector2(stateMachine.Rigidbody.velocity.x, Mathf.Max(stateMachine.Rigidbody.velocity.y, -stateMachine.ControllerData.MaxFallSpeed));
            }
            else {
                //Default gravity if standing on a platform or moving upwards
                SetGravityScale(stateMachine.ControllerData.GravityScale);
            }
        }
        else {
            //No gravity when dashing (returns to normal once initial dashAttack phase over)
            SetGravityScale(0);
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void AddForce(Transform other, float knockback, float upKnockback){

        Vector3 enemyDir;

        if(other.transform.position.x - stateMachine.transform.position.x > 0.1f){
            enemyDir = Vector3.left;

            stateMachine.Rigidbody.AddForce(enemyDir * knockback, ForceMode2D.Force);
            stateMachine.Rigidbody.AddForce(Vector3.up * upKnockback, ForceMode2D.Force);
        }

        if(other.transform.position.x - stateMachine.transform.position.x < 0.1f){
            enemyDir = Vector3.right;

            stateMachine.Rigidbody.AddForce(enemyDir * knockback, ForceMode2D.Force);
            stateMachine.Rigidbody.AddForce(Vector3.up * upKnockback, ForceMode2D.Force);
        }
    }

    public void SetGravityScale(float scale) {
        stateMachine.Rigidbody.gravityScale = scale;
    }
}

