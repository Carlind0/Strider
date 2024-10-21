using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Idle_Behavior : StateMachineBehaviour
{
    private int rand;
    public float timer;
    public float minTime;
    public float maxTime;

    Transform player;
    Rigidbody2D rb;
    public float attack2Range;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = Random.Range(minTime, maxTime);
        rb = animator.GetComponent<Rigidbody2D>();
        rand = Random.Range(0, 2);

        if (rand == 0)
        {
            animator.SetTrigger("Attack 2");
        }
        else
        {
            animator.SetTrigger("CanRun");
        }
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("CanRun");
            animator.SetTrigger("Attack 2");
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (Vector2.Distance(player.position, rb.position) <= attack2Range)
        {
            animator.SetTrigger("Attack 2");
        }
    }

     
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack 2");
    }
}
