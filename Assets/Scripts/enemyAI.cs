using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{

    public Transform target;
    public float moveSpeed;
    private Rigidbody2D rb;
    public float attackPower;
    public float health;

    public bool ranged;
    public float attemptedOuterDistance;
    public float attemptedInnerDistance;
    public float firingOuterRange;
    public float firingInnerRange;

    public GameObject projectilePrefab;
    public float attackCooldown;
    public float attackCooldownRemaining;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        if (Vector3.Distance(transform.position, target.position) > attemptedOuterDistance)
        {
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
        }
        else if(Vector3.Distance(transform.position, target.position) < attemptedInnerDistance)
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }

        if(ranged)
        {
            if(Vector3.Distance(transform.position, target.position) < firingOuterRange && Vector3.Distance(transform.position, target.position) > firingInnerRange)
            {
                if (attackCooldownRemaining <= 0)
                {
                    attackCooldownRemaining = attackCooldown;
                    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    ProjectileData proj = projectile.GetComponent<ProjectileData>();
                    proj.direction = (target.position - transform.position).normalized;
                    proj.damage = attackPower;
                }
                else 
                    attackCooldownRemaining -= Time.deltaTime;
            }
        }

        if(health <= 0)
        {
            Destroy(this);
        }
    }

    public void onTakeDamage(float damage, float knockback)
    {
        health -= damage;
        Debug.Log(health);
        transform.Translate(new Vector2(-knockback,0));
    }

    public void hit(Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}
