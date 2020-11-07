using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    public GameObject lockedDoor;
    public GameObject closedDoor;
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

    private float waitTime = 15f;
    private float deathTime = 5f;

    private Collider2D collide;
    // Start is called before the first frame update
    void Start()
    {
        collide = gameObject.GetComponent<Collider2D>();
        Invoke("checkDoors", 6f);
        Invoke("spawnDoors", 8f);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (waitTime > 0)
    //        waitTime -= Time.deltaTime;
    //    else
    //    {
    //        if(deathTime > 0)
    //        {
    //            deathTime -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            Destroy(collide);
    //        }
    //        if(topDoor)
    //        {
    //            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
    //            if (roomCollider == null)
    //                topDoorType = 0;
    //            else
    //            {
    //                GameObject room = roomCollider.gameObject;
    //                DoorCheck otherDoor = room.GetComponent<DoorCheck>();
    //                if (otherDoor.bottomDoor && !topDoorSpawned)
    //                {
    //                    topDoorType = 1;
    //                    GameObject door = Instantiate(closedDoor, transform.position, Quaternion.identity);
    //                    door.transform.parent = gameObject.transform;
    //                    Vector3 pos = door.transform.position;
    //                    pos.y += 8;
    //                    door.transform.position = pos;
    //                    topDoorSpawned = true;
    //                }
    //                else
    //                    topDoorType = 2;
    //            }
    //        }
    //        if (rightDoor)
    //        {
    //            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
    //            if (roomCollider == null)
    //                rightDoorType = 0;
    //            else
    //            {
    //                GameObject room = roomCollider.gameObject;
    //                DoorCheck otherDoor = room.GetComponent<DoorCheck>();
    //                if (otherDoor.leftDoor && !rightDoorSpawned)
    //                { 
    //                    rightDoorType = 1;
    //                    GameObject door = Instantiate(closedDoor, transform.position, Quaternion.identity);
    //                    door.transform.parent = gameObject.transform;
    //                    Vector3 pos = door.transform.position;
    //                    pos.x += 8;
    //                    door.transform.position = pos;
    //                    rightDoorSpawned = true;
    //                }
    //                else
    //                    rightDoorType = 2;
    //            }
    //        }
    //        if (bottomDoor)
    //        {
    //            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
    //            if (roomCollider == null)
    //                bottomDoorType = 0;
    //            else
    //            {
    //                GameObject room = roomCollider.gameObject;
    //                DoorCheck otherDoor = room.GetComponent<DoorCheck>();
    //                if (otherDoor.topDoor && !bottomDoorSpawned)
    //                {
    //                    bottomDoorType = 1;
    //                    GameObject door = Instantiate(closedDoor, transform.position, Quaternion.identity);
    //                    door.transform.parent = gameObject.transform;
    //                    Vector3 pos = door.transform.position;
    //                    pos.y -= 8;
    //                    door.transform.position = pos;
    //                    bottomDoorSpawned = true;
    //                }
    //                else
    //                    bottomDoorType = 2;
    //            }
    //        }
    //        if (leftDoor)
    //        {
    //            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
    //            if (roomCollider == null)
    //                leftDoorType = 0;
    //            else
    //            {
    //                GameObject room = roomCollider.gameObject;
    //                DoorCheck otherDoor = room.GetComponent<DoorCheck>();
    //                if (otherDoor.rightDoor && !leftDoorSpawned)
    //                {
    //                    leftDoorType = 1;
    //                    GameObject door = Instantiate(closedDoor, transform.position, Quaternion.identity);
    //                    door.transform.parent = gameObject.transform;
    //                    Vector3 pos = door.transform.position;
    //                    pos.x -= 8;
    //                    door.transform.position = pos;
    //                    leftDoorSpawned = true;
    //                }
    //                else
    //                    leftDoorType = 2;
    //            }
    //        }
    //    }
    //}
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
        }
        else if(topDoorType == 2)
        {
            GameObject door = Instantiate(lockedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.y += 8;
            door.transform.position = pos;
            topDoorSpawned = true;
        }

        if(rightDoorType == 1)
        {
            GameObject door = Instantiate(closedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.x += 8;
            door.transform.position = pos;
            rightDoorSpawned = true;
        }
        else if (rightDoorType == 2)
        {
            GameObject door = Instantiate(lockedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.x += 8;
            door.transform.position = pos;
            rightDoorSpawned = true;
        }

        if (bottomDoorType == 1)
        {
            GameObject door = Instantiate(closedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.y -= 8;
            door.transform.position = pos;
            bottomDoorSpawned = true;
        }
        else if (bottomDoorType == 2)
        {
            GameObject door = Instantiate(lockedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.y -= 8;
            door.transform.position = pos;
            bottomDoorSpawned = true;
        }

        if (leftDoorType == 1)
        {
            GameObject door = Instantiate(closedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.x -= 8;
            door.transform.position = pos;
            leftDoorSpawned = true;
        }
        else if (leftDoorType == 2)
        {
            GameObject door = Instantiate(lockedDoor, transform.position, Quaternion.identity);
            door.transform.parent = gameObject.transform;
            Vector3 pos = door.transform.position;
            pos.x -= 8;
            door.transform.position = pos;
            leftDoorSpawned = true;
        }
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
            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
            if (roomCollider == null)
                rightDoorType = 0;
            else
            {
                GameObject room = roomCollider.gameObject;
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
            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
            if (roomCollider == null)
                bottomDoorType = 0;
            else
            {
                GameObject room = roomCollider.gameObject;
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
            Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
            if (roomCollider == null)
                leftDoorType = 0;
            else
            {
                GameObject room = roomCollider.gameObject;
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

