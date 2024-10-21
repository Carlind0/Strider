using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Animations;

public class Boss_Run : StateMachineBehaviour
{
    
    public float speed;
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    public float attackRange = 3f;
    public float timer;
    public float minTime;
    public float maxTime;
    

    



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player =GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        timer = Random.Range(minTime, maxTime);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (timer <= 0)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }
            boss.LookAtPlayer();
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
            }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}




