using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private float time = 15;
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }

    void Update()
    {
        if (time <= 15)
            Destroy(gameObject);
        else
            time -= Time.deltaTime;
    }

}
