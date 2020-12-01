using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossAI : MonoBehaviour
{
    //Stats
    public float moveSpeed;
    public bool ranged;
    public float attackPower;
    public float health;
    public float attemptedOuterDistance;
    public float attemptedInnerDistance;
    public float firingOuterRange;
    public float firingInnerRange;
    public float attackCooldown;
    public float attackCooldownRemaining;

    //Components
    public Transform target;
    private Rigidbody2D rb;
    public GameObject projectilePrefab;

    //SFX Components
    public AudioSource audioSource;
    public AudioClip attackSFX;
    public AudioClip takeDamageSFX;
    public AudioClip dieSFX;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        //Lookat player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        //Attacks
        if (ranged)
            shoot();
            
    }

    //Function to a projectile shoot at player
    void shoot()
    {
        if (Vector3.Distance(transform.position, target.position) < firingOuterRange && Vector3.Distance(transform.position, target.position) > firingInnerRange)
        {
            if (attackCooldownRemaining <= 0)
            {
                attackCooldownRemaining = attackCooldown;
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                ProjectileData proj = projectile.GetComponent<ProjectileData>();
                proj.direction = (target.position - transform.position).normalized;
                proj.damage = attackPower;

                audioSource.PlayOneShot(attackSFX);
            }
            else
                attackCooldownRemaining -= Time.deltaTime;
        }
    }

    //Update dealing with Rigidbody
    void FixedUpdate()
    {
        Vector3 lastPos = transform.position;

        //If too far from player, move closer
        if (Vector3.Distance(transform.position, target.position) > attemptedOuterDistance)
        {
            rb.AddRelativeForce(transform.right * moveSpeed);
        }

        //If too close to player, move farther
        else if (Vector3.Distance(transform.position, target.position) < attemptedInnerDistance)
        {
            rb.AddRelativeForce(transform.right * -moveSpeed);
        }

        //If unable/unwilling to move front/back, move left/right
        if(transform.position == lastPos)
        {
            int dir = Random.Range(0, 2);

            switch(dir)
            {
                case 0:
                    rb.AddRelativeForce(transform.up * 2*moveSpeed);
                    break;
                case 1:
                    rb.AddRelativeForce(transform.up * 2* -moveSpeed);
                    break;
            }
        }
    }


    //When taking damage
    public void onTakeDamage(float damage, float knockback)
    {
        health -= damage;
        transform.Translate(new Vector2(-knockback,0));

        if(health > 0)
        {
            audioSource.PlayOneShot(takeDamageSFX);
        }

        //If health = 0, die
        if(health <= 0)
        {
            audioSource.PlayOneShot(dieSFX);

            SceneManager.LoadScene("GameOver");
        }
    }


    public void hit(Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}
