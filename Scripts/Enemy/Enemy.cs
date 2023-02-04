using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 100;
    int currentHealth;
    public Rigidbody2D rb;
    public Transform player;
    public float speed;
    public float agro;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent <Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distoPlayer = Vector2.Distance(transform.position, player.position);

        if (distoPlayer < agro)
        {
            StartHunting();
        }
        else
        {
            StopHunting();
        }
    }

    void StartHunting()
    {
            anim.SetBool("Walk", false);
        if(player.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if(player.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(-1, 1);
            
        }
    }

    void StopHunting()
    {
        anim.SetBool("Walk", true);
        rb.velocity = new Vector2(0, 0);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        rb. bodyType= RigidbodyType2D.Static;
    }


}
