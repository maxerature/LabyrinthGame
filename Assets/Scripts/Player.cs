using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Base Stats
    public float moveSpeed = 10f;
    public float maxHealth;
    public float health;
    public float range;
    public float damage;
    public float knockback;
    public float invincibilityTime;
    public float regenRate;
    public float regenTimer;

    //Components
    private Rigidbody2D rb;
    Camera viewCamera;
    public GameObject bulletPrefab;
    public GameObject healthBar;
    [SerializeField] private FieldOfView fieldOfView;

    //Invincibility
    private bool invincible = false;
    public float invincibilityTimeRemaining;

    //Item Stats
    public List<GameObject> items;
    public float killRegen;
    public float damRegen;
    

    // Start is called before the first frame update
    void Start()
    {
        //Setup Components
        rb = gameObject.GetComponent<Rigidbody2D>();
        viewCamera = Camera.main;
        viewCamera.enabled = false;


        //Activate functions with delay
        Invoke("levelSetup", 7f);
    }

    //Sets up the game after level generation.
    void levelSetup()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        transform.position = pos;
        viewCamera.enabled = true;
    }

    //Regenerate
    void Regenerate()
    {
        if (health < maxHealth)
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0)
            {
                health += 1;
                float healthPerc = health / maxHealth;
                HealthBar hbscript = healthBar.GetComponent<HealthBar>();
                hbscript.SetSize(healthPerc);

                regenTimer = regenRate;
            }
        }
    }

    //Shoot bullet
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        PlayerBullet proj = bullet.GetComponent<PlayerBullet>();
        proj.direction = (aimDir).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir, range, 1 << LayerMask.NameToLayer("Targets"));
        if (hit.collider != null)
        {
            Debug.Log(hit.collider);
            GameObject other = hit.collider.gameObject;
            enemyAI otherScript = other.GetComponent<enemyAI>();
            Debug.Log(other);
            if (otherScript != null)
            {
                otherScript.onTakeDamage(damage, knockback);
            }
            else
                Debug.Log("no component");
        }
        else
        {
            Debug.Log("nothing hit");
        }
    }
        

    void Update() {
        //Regeneration
        Regenerate();
        
        //Destroy script on death
        if(health <= 0)
        {
            Destroy(this);
        }

        //Get Aim direction and rotate
        Vector3 targetPosition = GetMouseWorldPosition();
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        Vector3 vectorToTarget = targetPosition - transform.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        transform.rotation = targetRotation;

        //Rotate field of view.
        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);

        //Shoot on LMB
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        //Reduce Invincibility Timer
        if (invincible)
        {
            invincibilityTimeRemaining -= Time.deltaTime;
            if (invincibilityTimeRemaining <= 0)
                invincible = false;
        }
    }


    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {

            // convert user input into world movement
            float horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
            float verticalMovement = Input.GetAxisRaw("Vertical") * moveSpeed;

            //assign world movements to a Veoctor2
            Vector2 directionOfMovement = new Vector2(horizontalMovement, verticalMovement);

            // apply movement to player's transform
            rb.AddForce(directionOfMovement);

        }
    }

    //Helper functions for aiming
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }


    //Collision with hurtbox
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            if (!invincible)
            {
                Vector2 kb = new Vector2(0, 0);
                if (col.gameObject.tag == "Enemy")
                {
                    enemyAI otherScript = col.gameObject.GetComponent<enemyAI>();
                    health -= otherScript.attackPower;
                    kb = (col.gameObject.transform.position - transform.position).normalized * 10;
                }
                else if (col.gameObject.tag == "EnemyProjectile")
                {
                    ProjectileData otherScript = col.gameObject.GetComponent<ProjectileData>();
                    health -= otherScript.damage;
                    kb = (col.gameObject.transform.position - transform.position).normalized * 1;
                }
                rb.AddForce(-kb, ForceMode2D.Impulse);
                invincible = true;
                invincibilityTimeRemaining = invincibilityTime;

                float healthPerc = health / maxHealth;
                HealthBar hbscript = healthBar.GetComponent<HealthBar>();
                hbscript.SetSize(healthPerc);
            }
        }
    }
}