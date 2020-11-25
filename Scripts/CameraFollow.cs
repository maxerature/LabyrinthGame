using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float followDist;
    public float followSpeed;
    public GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 convertedPos = transform.position;
        convertedPos.z = 0;

        float dist = Vector3.Distance(player.transform.position, convertedPos);
        if(dist > followDist)
        {
            Vector2 angle = (player.transform.position - convertedPos).normalized;
            rb.AddForce(new Vector2(angle.x, angle.y) * followSpeed);
        }


        //Vector3 temp = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        //transform.position = temp;
    }
}
