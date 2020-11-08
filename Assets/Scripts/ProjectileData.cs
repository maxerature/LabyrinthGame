using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    public float damage;
    public float speed;
    public Vector2 direction;
    public float lifeSpan;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
        
    }

    void Update()
    {
        if (lifeSpan >= 0)
        {
            lifeSpan -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(direction * speed);
    }
}
