using UnityEngine;
using UnityEngine.AI;


public class RangeForceReceiver : MonoBehaviour{

    [SerializeField] private RangeStateMachine stateMachine;
    [SerializeField] private float drag = 0.3f;

    private Vector2 impact;
    private Vector3 dampingVelocity;

    public Vector2 Movement => impact;

    private void Update(){

        SetGravityScale(stateMachine.ControllerData.GravityScale);

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void AddForce(Transform other, float knockback){

        Vector3 playerDir;

        if(stateMachine.playerStateMachine != null){

                if(stateMachine.playerStateMachine.transform.position.x - stateMachine.transform.position.x > 0.1f){
                playerDir = Vector3.left;

                stateMachine.Rigidbody.AddForce(playerDir * knockback, ForceMode2D.Force);
            }

                if(stateMachine.playerStateMachine.transform.position.x - stateMachine.transform.position.x < 0.1f){
                playerDir = Vector3.right;

                stateMachine.Rigidbody.AddForce(playerDir * knockback, ForceMode2D.Force);
            }
        }
        if(stateMachine.astralStateMachine != null){

            if(stateMachine.astralStateMachine.transform.position.x - stateMachine.transform.position.x > 0.1f){
                playerDir = Vector3.left;

                stateMachine.Rigidbody.AddForce(playerDir * knockback, ForceMode2D.Force);
            }

            if(stateMachine.astralStateMachine.transform.position.x - stateMachine.transform.position.x < 0.1f){
                playerDir = Vector3.right;

                stateMachine.Rigidbody.AddForce(playerDir * knockback, ForceMode2D.Force);
            }
        }
    }

    public void SetGravityScale(float scale) {
        stateMachine.Rigidbody.gravityScale = scale;
    }
}

