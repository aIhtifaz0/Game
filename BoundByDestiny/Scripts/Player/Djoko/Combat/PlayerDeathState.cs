using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathState : PlayerBaseState{

    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Death");
    private const float TransitionDuration = 0.1f;

    public override void Enter() {
        
        stateMachine.Rigidbody.velocity = Vector3.zero;
        
        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);

        //disable weapon collider
        stateMachine.Weapon.gameObject.SetActive(false);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //Object.Destroy(stateMachine.gameObject, 1f);
    }

    public override void Tick(float deltatime) {}

    public override void Exit() {

        
    }
}
