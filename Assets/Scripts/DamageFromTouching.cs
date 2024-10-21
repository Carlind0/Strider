using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromTouching : MonoBehaviour
{
    public PlayerHealth pHealth;
    public int  damage;
    // Start is called before the first frame update
    void Start()
    {
        pHealth = gameObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision/*other*/)
    {

        if  (collision.gameObject.tag == "Player")
        {
            pHealth= collision.gameObject.GetComponent<PlayerHealth>();
            pHealth.TakeDamage(damage);
        }

        /*if (other.gameObject.CompareTag("Player"))
        {
           other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }*/
    }
}
