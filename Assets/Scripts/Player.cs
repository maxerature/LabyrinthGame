using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    //public Animator animation;
    public float maxHealth;
    public float health;
    public float range;
    public float damage;
    public float knockback;
    public float invincibilityTime;

    public float regenRate;
    public float regenTimer;

    private Rigidbody2D rb;

    private bool invincible = false;
    public float invincibilityTimeRemaining;
    public GameObject bulletPrefab;

    Camera viewCamera;

    public GameObject healthBar;
    [SerializeField] private FieldOfView fieldOfView;



    // Start is called before the first frame update
    void Start()
    {
        
        rb = gameObject.GetComponent<Rigidbody2D>();

        viewCamera = Camera.main;

        viewCamera.enabled = false;

        //animation = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("Player::Start cant find RigidBody2D </sadface>");
        }
        rb.drag = 5;
        rb.gravityScale = 0;

        //gameObject.SetActive(false);
        Invoke("levelSetup", 7f);
    }

    void levelSetup()
    {
        //gameObject.SetActive(true);
        Vector3 pos = new Vector3(0, 0, 0);
        transform.position = pos;
        viewCamera.enabled = true;
    }
        
    void Update() {
        if(health < maxHealth)
        {
            regenTimer -= Time.deltaTime;
            if(regenTimer <= 0)
            {
                health += 1;
                float healthPerc = health / maxHealth;
                HealthBar hbscript = healthBar.GetComponent<HealthBar>();
                hbscript.SetSize(healthPerc);

                regenTimer = regenRate;
            }
        }
        if(health <= 0)
        {
            Destroy(this);
        }

        Vector3 targetPosition = GetMouseWorldPosition();
        Vector3 aimDir = (targetPosition - transform.position).normalized;

        Vector3 vectorToTarget = targetPosition - transform.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        transform.rotation = targetRotation;

        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);


        if (Input.GetMouseButtonDown(0))
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