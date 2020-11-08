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

        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 1) * direction;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        transform.rotation = targetRotation;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
