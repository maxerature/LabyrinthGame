﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    public GameObject lockedDoor;
    public GameObject closedDoor;
    public GameObject openDoorPrefab;

    public GameObject[] enemyTypes;

    public bool topDoor;
    public bool rightDoor;
    public bool bottomDoor;
    public bool leftDoor;

    public bool topDoorSpawned;
    public bool rightDoorSpawned;
    public bool bottomDoorSpawned;
    public bool leftDoorSpawned;

    // Type of door.  0=none, 1=normal, 2=locked
    public int topDoorType;
    public int rightDoorType;
    public int bottomDoorType;
    public int leftDoorType;

    private float deathTime = 5f;

    public List<GameObject> enemies;
    public List<GameObject> doors;
    public List<GameObject> unlockedDoors;

    private Collider2D collide;

    public GameObject topRoom;
    public GameObject rightRoom;
    public GameObject bottomRoom;
    public GameObject leftRoom;

    private bool opened;
    public bool safeRoom;

    public int enemyCount;


    // Start is called before the first frame update
    void Start()
    {
        collide = gameObject.GetComponent<Collider2D>();
        opened = false;
        Invoke("checkDoors", 6f);
        Invoke("spawnDoors", 6.5f);
        Invoke("spawnEnemies", 6.5f);
    }

    void Update()
    {
        
        if(enemyCount == 0  && !opened)
        {
            for(int i=0; i<unlockedDoors.Count; i++)
            {
                opened = true;
                GameObject door = unlockedDoors[i];
                GameObject openDoor = Instantiate(openDoorPrefab, door.transform.position, door.transform.rotation);
                unlockedDoors[i] = openDoor;
                openDoor.transform.parent = gameObject.transform;
                Destroy(door);
            }
        }
    }

    void spawnEnemies()
    {
        if(!safeRoom)
        {
            enemyCount = Random.Range(2, 10);
            int randType;

            for(int i=0; i<enemyCount; i++)
            {
                randType = Random.Range(0, enemyTypes.Length);
                GameObject enemy = Instantiate(enemyTypes[randType], transform.position, Quaternion.identity);
                enemy.transform.parent = gameObject.transform;

                Vector3 pos = new Vector3(Random.Range(-6, 6), Random.Range(-6, 6), 0);
                pos = enemy.transform.position + pos;
                enemy.transform.position = pos;
                enemies.Add(enemy);
            }
        }
    }

    public void enemyKilled()
    {
        for(int i=enemies.Count-1; i >= 0; i--)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    void spawnDoors()
    {
        if(topDoorType == 1)
        {
            GameObject door = Instantiate(closedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.y += 8;
            door.transform.position = pos;
            topDoorSpawned = true;
            doors.Add(door);
            unlockedDoors.Add(door);
        }
        else if(topDoorType == 2)
        {
            GameObject door = Instantiate(lockedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.y += 8;
            door.transform.position = pos;
            topDoorSpawned = true;
            doors.Add(door);
        }

        if(rightDoorType == 1)
        {
            GameObject door = Instantiate(closedDoor, transform.position, Quaternion.Euler(0, 0, -90));
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.x += 8;
            door.transform.position = pos;
            rightDoorSpawned = true;
            doors.Add(door);
            unlockedDoors.Add(door);
        }
        else if (rightDoorType == 2)
        {
            GameObject door = Instantiate(lockedDoor, transform.position, Quaternion.Euler(0, 0, -90));
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.x += 8;
            door.transform.position = pos;
            rightDoorSpawned = true;
            doors.Add(door);
        }

        if (bottomDoorType == 1)
        {
            GameObject door = Instantiate(closedDoor, transform.position, Quaternion.Euler(0, 0, 180));
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.y -= 8;
            door.transform.position = pos;
            bottomDoorSpawned = true;
            doors.Add(door);
            unlockedDoors.Add(door);
        }
        else if (bottomDoorType == 2)
        {
            GameObject door = Instantiate(lockedDoor, transform.position, Quaternion.Euler(0, 0, 180));
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.y -= 8;
            door.transform.position = pos;
            bottomDoorSpawned = true;
            doors.Add(door);
        }

        if (leftDoorType == 1)
        {
            GameObject door = Instantiate(closedDoor, transform.position, Quaternion.Euler(0, 0, -270));
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.x -= 8;
            door.transform.position = pos;
            leftDoorSpawned = true;
            doors.Add(door);
            unlockedDoors.Add(door);
        }
        else if (leftDoorType == 2)
        {
            GameObject door = Instantiate(lockedDoor, transform.position, Quaternion.Euler(0, 0, -270));
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.x -= 8;
            door.transform.position = pos;
            leftDoorSpawned = true;
            doors.Add(door);
        }
        Destroy(collide);
    }


    void checkDoors()
    {
        if (topDoor)
        {
            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
            if (roomCollider == null)
                topDoorType = 0;
            else
            {
                GameObject room = roomCollider.gameObject;
                topRoom = room.transform.parent.transform.gameObject;
                DoorCheck otherDoor = room.GetComponent<DoorCheck>();
                if (otherDoor.bottomDoor)
                {
                    topDoorType = 1;
                }
                else
                    topDoorType = 2;
            }
        }
        if (rightDoor)
        {
            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x + 20, transform.position.y), 0.25f);
            if (roomCollider == null)
                rightDoorType = 0;
            else
            {
                GameObject room = roomCollider.gameObject;
                rightRoom = room.transform.parent.transform.gameObject;
                DoorCheck otherDoor = room.GetComponent<DoorCheck>();
                if (otherDoor.leftDoor)
                {
                    rightDoorType = 1;
                }
                else
                    rightDoorType = 2;
            }
        }
        if (bottomDoor)
        {
            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 20), 0.25f);
            if (roomCollider == null)
                bottomDoorType = 0;
            else
            {
                GameObject room = roomCollider.gameObject;
                bottomRoom = room.transform.parent.transform.gameObject;
                DoorCheck otherDoor = room.GetComponent<DoorCheck>();
                if (otherDoor.topDoor)
                {
                    bottomDoorType = 1;
                }
                else
                    bottomDoorType = 2;
            }
        }
        if (leftDoor)
        {
            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x - 20, transform.position.y), 0.25f);
            if (roomCollider == null)
                leftDoorType = 0;
            else
            {
                GameObject room = roomCollider.gameObject;
                leftRoom = room.transform.parent.transform.gameObject;
                DoorCheck otherDoor = room.GetComponent<DoorCheck>();
                if (otherDoor.rightDoor)
                {
                    leftDoorType = 1;
                }
                else
                    leftDoorType = 2;
            }
        }
    }
}

