using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    public float baseMoveSpeed = 10f;
    public float baseMaxHealth;
    public float health;
    public float baseRange;
    public float baseDamage;
    public float baseKnockback;
    public float baseRegenRate;
    public float baseRegenTimer;

    //Components
    private Rigidbody2D rb;
    Camera viewCamera;
    public GameObject bulletPrefab;
    public GameObject healthBar;
    [SerializeField] private FieldOfView fieldOfView;
    private HealthBar hbscript;
    public GameObject canvas;
    public AudioSource audioSource;
    public AudioClip gunShot;

    //Invincibility and Timers
    public float invincibilityTime;
    private bool invincible = false;
    public float invincibilityTimeRemaining;
    public float regenTimerRemaining;

    //Item Stats
    public List<string> items;
    public float killRegen;
    public float damTimerDec;
    public float moveSpeed;
    public float maxHealth;
    public float range;
    public float damage;
    public float knockback;
    public float regenRate;
    public float regenTimer;

    private bool activateMouse;
    


    // Start is called before the first frame update
    void Start()
    {
        //Setup Components
        rb = gameObject.GetComponent<Rigidbody2D>();
        viewCamera = Camera.main;
        viewCamera.enabled = false;
        activateMouse = false;
        hbscript = healthBar.GetComponent<HealthBar>();


        //Activate functions with delay
        Invoke("levelSetup", 10f);

        //Set stats to base stats
        moveSpeed = baseMoveSpeed;
        maxHealth = baseMaxHealth;
        health = maxHealth;
        range = baseRange;
        damage = baseDamage;
        knockback = baseKnockback;
        regenRate = baseRegenRate;
        regenTimer = baseRegenTimer;
        HUDController hud = canvas.GetComponent<HUDController>();
        hud.setHealthText(health, maxHealth);
        hud.updateTexts((float)range / baseRange, (float)damage / baseDamage, (float)knockback / baseKnockback, (float)regenRate / baseRegenRate, (float)killRegen, (float)damTimerDec, (float)moveSpeed / baseMoveSpeed);
    }

    //Sets up the game after level generation.
    void levelSetup()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        transform.position = pos;
        viewCamera.enabled = true;

        //Stats Setup (Duplicate)
        moveSpeed = baseMoveSpeed;
        maxHealth = baseMaxHealth;
        range = baseRange;
        damage = baseDamage;
        knockback = baseKnockback;
        regenRate = baseRegenRate;
        regenTimer = baseRegenTimer;
        activateMouse = true;
        health = maxHealth;
        hbscript.SetSize(1f);
    }

    //Regenerate
    void Regenerate()
    {
        if (health < maxHealth)
        {
            regenTimerRemaining -= Time.deltaTime;
            if (regenTimerRemaining <= 0)
            {
                health += regenRate;
                if (health > maxHealth)
                    health = maxHealth;
                float healthPerc = health / maxHealth;
                hbscript.SetSize(healthPerc);
                HUDController hud = canvas.GetComponent<HUDController>();
                hud.setHealthText(health, maxHealth);

                regenTimerRemaining = regenTimer;
            }
        }
    }

    //Shoot bullet
    void Shoot(Vector3 aimDir)
    {
        Vector3 pos = transform.position;
        pos += transform.right * 0.5f;
        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        PlayerBullet proj = bullet.GetComponent<PlayerBullet>();
        proj.direction = (aimDir).normalized;

        audioSource.pitch = Random.Range(0.7f, 1.1f);
        audioSource.PlayOneShot(gunShot);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir, range, 1 << LayerMask.NameToLayer("Targets"));
        if (hit.collider != null)
        {
            GameObject other = hit.collider.gameObject;
            enemyAI otherScript = other.GetComponent<enemyAI>();
            if (otherScript != null)
            {
                otherScript.onTakeDamage(damage, knockback);
            }
        }
    }
        

    void Update() {
        //Regeneration
        Regenerate();

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        
        //Destroy script on death
        if(health <= 0)
        {
            SceneManager.LoadScene("Menu");
            Destroy(this);
        }

        if (activateMouse)
        {
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
                Shoot(aimDir);
            }
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
        //If enemy
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
                HUDController hud = canvas.GetComponent<HUDController>();
                hud.setHealthText(health, maxHealth);
            }
        }

        //If item
        else if(col.gameObject.tag == "Item")
        {
            GameObject item = col.gameObject;
            Destroy(col);
            items.Add(item.name);
            ItemHandler ih = item.GetComponent<ItemHandler>();
            ih.onPickup(gameObject, this);
            HUDController hud = canvas.GetComponent<HUDController>();
            hud.updateTexts((float)range / baseRange, (float)damage / baseDamage, (float)knockback / baseKnockback, (float)regenRate / baseRegenRate, (float)killRegen, (float)damTimerDec, (float)moveSpeed/baseMoveSpeed);
            if (item)
            {
                Destroy(item);
            }
        }
    }

}