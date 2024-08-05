using UnityEngine;
using UnityEngine.AI;


public class EnemyForceReceiver : MonoBehaviour{

    [SerializeField] private MeleeStateMachine stateMachine;

    [SerializeField] private float drag = 0.3f;

    private Vector2 impact;
    private Vector3 dampingVelocity;

    public Vector2 Movement => impact;

    private void Update(){

        SetGravityScale(stateMachine.ControllerData.GravityScale);

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void AddForce(Vector2 force){
        
        impact += force;
    }

    public void SetGravityScale(float scale) {
        stateMachine.Rigidbody.gravityScale = scale;
    }
}

