using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;

    public Transform AttackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(Time.time >= nextAttackTime )
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");

    }

    public void OnAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);


        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        return;

        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
