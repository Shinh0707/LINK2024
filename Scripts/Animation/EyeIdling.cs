using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EyeIdling : StateMachineBehaviour
{
    private float nothingTimer;
    private float IdlingTimer;
    private float startIdingTimer;
    [SerializeField] string SpeedMultiplerParameter;
    private float flip = 1f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetTimers(animator, stateInfo);
    }
    //EyeIdlingModifier
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (nothingTimer > 0) {
            nothingTimer -= Time.deltaTime;
            if (nothingTimer <= 0) {
                animator.SetFloat(SpeedMultiplerParameter, 1f);
            }
        }
        else if(IdlingTimer > 0)
        {
            IdlingTimer -= Time.deltaTime;
            if (IdlingTimer <= 0)
            {
                if (flip == 1) {
                    flip *= -1;
                    IdlingTimer = startIdingTimer;
                    animator.SetFloat(SpeedMultiplerParameter, flip);
                }
                else
                {
                    ResetTimers(animator, stateInfo);
                }
            }
        }
    }
    public void ResetTimers(Animator animator, AnimatorStateInfo stateInfo)
    {
        nothingTimer = Random.Range(3f, 20f);
        IdlingTimer = Random.Range(0.1f, 0.5f);
        startIdingTimer = IdlingTimer;
        flip = 1f;
        animator.SetFloat(SpeedMultiplerParameter, 0f);
    }
}
