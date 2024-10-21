using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource hurtSound;
    public int MaxHealth =100;
    public int Health;
    public Animator animator;
    public Health_Bar healthBar;
    public float iFrameDuration;
    public float numberOfFlashes;
    private SpriteRenderer spriteRend;
    private CinemachineImpulseSource impulseSource;
    public bool CanTakeDamage;


    
  
    public GameOver GameOver;
   
    void Start()
    {
        CanTakeDamage = true;
        Physics2D.IgnoreLayerCollision(6, 9, false);
        //sets max health and health to the same so player starts with full health
        Health = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        spriteRend = GetComponent<SpriteRenderer>();
       impulseSource = GetComponent<CinemachineImpulseSource>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        if (CanTakeDamage == false)
        {
            Physics2D.IgnoreLayerCollision(6, 9, true);
        }
        
       
    }
    //Take damage function (a number to damage)
    public void TakeDamage(int damage)
    {
        if (CanTakeDamage)
        {
        hurtSound.Play();
        animator.SetTrigger("Hurt");
        Health -= damage;
        CameraShake.instance.CameraShakeImpulse(impulseSource);
        healthBar.SetHealth(Health);
        if (Health > 0)
        {
            //this happens so when player gets hit he gets some Invunerability frames
            StartCoroutine(Invunerability());
        }

        if (Health <= 0)
        {
            //no health player dead
            
            Destroy(gameObject);
            GameOver.gameOver();
           
        }

        }
    }
    //Invunerability frames script, this will make the player be invunerable for a certain amount of time after getting hit
    private IEnumerator Invunerability()
    {
        //6 is Player Layer in the inspector, 9 is the Enemy layer in the inspector, so this line will disable colisions between those 2 layers
        CanTakeDamage =false;
        Physics2D.IgnoreLayerCollision(6, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            //waits for the flashes to occur to perform next line
            yield return new WaitForSeconds(iFrameDuration/(numberOfFlashes * 2 ));
            spriteRend.color = Color.white;
            //waits for the flashes to occur to perform next line
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        CanTakeDamage = true;
        Physics2D.IgnoreLayerCollision(6, 9, false);
        //6 is Player Layer in the inspector, 9 is the Enemy layer in the inspector, so this line will return colisions between those 2 layers

    }
   
}

