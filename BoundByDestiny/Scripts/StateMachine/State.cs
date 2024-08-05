using UnityEngine;
using System;

public abstract class State{
    
    public abstract void Enter();
    public abstract void Tick(float deltatime);
    public abstract void Exit();

    protected float GetNormalizedTime(Animator animator, String tag) {

        AnimatorStateInfo CurrentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo NextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && NextInfo.IsTag(tag)) {

            return NextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && CurrentInfo.IsTag(tag)) {

            return CurrentInfo.normalizedTime;
        }
        else {

            return 0f;
        }
    }
}

/*
  State contains Enter, Tick & Exit

  Enter : Call once at start
  Tick : Call every frame
  Exit : Call at change state

  GetNormalizeTime :
  Getting is animation time at current frame for next animation or stop
*/
