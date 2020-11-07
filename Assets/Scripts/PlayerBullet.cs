using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public Vector2 direction;
    public float speed;

    private Rigidbody2D rb;

        
    // Start is called before the first frame update
    void Start()
    { 
        rb = gameObject.GetComponent<Rigidbody2D>();
        Invoke("die", 0.5f);
        direction = direction.normalized;
        transform.Rotate(direction);
    }

    void die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(direction * speed);
    }
}
