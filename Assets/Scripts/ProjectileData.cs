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
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(direction * speed);
        if (lifeSpan >= 0)
        {
            lifeSpan -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
