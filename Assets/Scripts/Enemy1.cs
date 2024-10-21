using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    public AudioSource hurtSound;
    public AudioSource DeathSound;
    Rigidbody2D rb;
    Animator animator;
    //public GameObject player;
    public float speed;
    private float distance;
    private float dazedTime;
    public float startDazedTime;
    public int maxHealth;
    public int Health;
    public Transform target;
    public bool HasTakenDamage {  get;  set; }
    public SpriteRenderer spriteRenderer;
    public PlayerHealth playerHealth;
    public GameObject collider2d;
   
    


    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(6, 9, false);
        Health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        rb = this.GetComponent<Rigidbody2D>();
        
    }

    

    // Update is called once per frame
    void Update()
    {

        

        EnemySpriteFlip();

        if (dazedTime <= 0 && Health >0) 
        {
            speed= 1;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        

        transform.position = Vector2.MoveTowards(transform.position,new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
        

       

    }
    


    //old damage method
    /*public void TakeDamage(int amount)
    {
        HasTakenDamage = true;
        dazedTime = startDazedTime;
        Health -= amount;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    */

    public void Damage(int damageAmount)
    {
        
        animator.SetTrigger("EnemyHurt");
        hurtSound.Play();
        HasTakenDamage = true;
        dazedTime = startDazedTime;
        Health -= damageAmount;
        if (Health <= 0)
        {
            
            
            ScoreScript.ScoreValue += 1;
           StartCoroutine(EnemyDying());
            
        }
    }

    private IEnumerator EnemyDying()
    {
        //Physics2D.IgnoreLayerCollision(6, 9, true);
        speed = 0;
        animator.SetTrigger("EnemyDeath");
        DeathSound.Play();
        collider2d.GetComponent<Rigidbody2D>().isKinematic = true;
       collider2d.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void EnemySpriteFlip()
    {
        Vector3 scale = transform.localScale;

        if (target.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

}
