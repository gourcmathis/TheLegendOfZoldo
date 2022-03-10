using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float lastAttackedAt = -9999f;
    public float cooldown = 1f;

    public Animator attackAnim;
    public Transform attackPos;
    public LayerMask whatIsEnnemies;
    public float attackRangeX;
    public float attackRangeY;
    public int damage;


    void Update()
    {
        
        if(Input.GetMouseButton(0))
        {
            if(Time.time > lastAttackedAt + cooldown)
            {
                Attack();
                lastAttackedAt = Time.time;
            }
        }        
    }

    void Attack()
    {
        attackAnim.SetTrigger("Attack");
        Invoke("delay",0.3f);
        
    }

    void delay(){
        Collider2D[] ennemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY), 0, whatIsEnnemies);
        for (int i = 0; i < ennemiesToDamage.Length; i++)
        {
            ennemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
