using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCallback : StateMachineBehaviour
{
    public bool IsInitialized = false;
    public Transform Pivot;
    public Transform PivotRoot;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!IsInitialized)
        {
            foreach (Transform child in animator.transform)
            {
                Pivot = child;
            }

            foreach (Transform child in Pivot)
            {
                PivotRoot = child;
            }

            IsInitialized = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PivotRoot.localRotation = Pivot.localRotation * PivotRoot.localRotation;
        Pivot.localRotation.Normalize();

        animator.Play("Idle");
        Debug.Log("Animation Done");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
