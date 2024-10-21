using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public AudioSource Slash1;
    public float timeBetweenAttack;
    //public float startTimeBetweenAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int PlayerDamage;
    public bool isAttacking {  get; set; }
    public int combo;
    public Animator animator;

    public bool ShouldBeDamaging {  get; set; } = false;

    private float attackCounter;

    private RaycastHit2D[] hits;

    private List<IDamageable> iDamageables = new List<IDamageable>();

    public bool otherMovement;


    public float delayAttack;
    



    // Start is called before the first frame update
    void Start()
    {
        
        otherMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& attackCounter >= timeBetweenAttack)
        {
            attackCounter = 0f;
            StartCoroutine(CheckAttackInput());
            
        }
        attackCounter += Time.deltaTime;

        //Attack();
    }
    public IEnumerator CheckAttackInput()
    {
        if (otherMovement ==false)//(Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            yield return new WaitForSeconds(delayAttack);
            isAttacking = true;
            
            animator.SetTrigger("" + combo);
            Slash1.Play();


        }
        
        

        
    }
   
    private void FixedUpdate()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }

    public IEnumerator DamageWhileSlashIsActive()
    {
        ShouldBeDamaging = true;

        while (ShouldBeDamaging)
        {
            
            hits = Physics2D.CircleCastAll(attackPos.position, attackRange, transform.right, 0f, whatIsEnemy);
            for (int i = 0; i < hits.Length; i++)
            {
                

                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                if (iDamageable != null && !iDamageable.HasTakenDamage)
                {
                    iDamageable.Damage(PlayerDamage);
                    iDamageables.Add(iDamageable);
                }
            }
            yield return null;
        }
        ReturnAttackablesToDamageable();
    }
    private void ReturnAttackablesToDamageable()
    {
        foreach(IDamageable EnemyDamaged in iDamageables)
        {
            EnemyDamaged.HasTakenDamage = false;
        }

        iDamageables.Clear();
    }
    /*public void Attack()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetButtonDown("Fire1")&& !isAttacking)
            {
                isAttacking = true;
                animator.SetTrigger(""+combo);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyAI>().TakeDamage(PlayerDamage);
                }



                timeBetweenAttack = startTimeBetweenAttack;

            }

        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }
    */


    public void Start_combo()
    {
        
        isAttacking =true;
        if (combo<1)
        {
            combo++;
        }
    }
    public void Finish_combo()
    {
    isAttacking=false ;
        combo=0 ;
    }
    
    public void ShouldBeDamagingToFalse()
    {
        ShouldBeDamaging = false;
    }
    public void otherMovementTrue()
    {
        otherMovement=true;
    }
    public void otherMovementFalse() { 
    otherMovement=false;}
}

