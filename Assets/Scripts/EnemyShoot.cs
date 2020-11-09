using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header ("Stats")]
    public float speed;
    public float stoppingDistance;
    public float nearDistance;
    public float fireRate;
    private float timeBtwShots;

    [Header ("References")]
    public GameObject shot;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //Enemy Movement
        //Moves Enemy Towards Player
        if(Vector2.Distance(transform.position, player.position) < nearDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
        //Moves Enemy Away from Player
        else if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        //Stops Moving when certain distance from player
        else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > nearDistance)
        {
            transform.position = this.transform.position;
        }

        //Enemy Shooting
        if(timeBtwShots <= 0)
        {
            Instantiate(shot, transform.position, Quaternion.identity);
            timeBtwShots = fireRate;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
