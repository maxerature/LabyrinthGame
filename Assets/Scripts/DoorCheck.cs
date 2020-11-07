using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    public bool topDoor;
    public bool rightDoor;
    public bool bottomDoor;
    public bool leftDoor;

    // Type of door.  0=none, 1=normal, 2=locked
    public int topDoorType;
    public int rightDoorType;
    public int bottomDoorType;
    public int leftDoorType;

    private float waitTime = 15f;
    private float deathTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime > 0)
            waitTime -= Time.deltaTime;
        else
        {
            if(deathTime > 0)
            {
                deathTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
            if(topDoor)
            {
                Collider2D roomCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 20), 0.25f);
                if (roomCollider == null)
                    topDoorType = 0;
                else
                {
                    GameObject room = roomCollider.gameObject;
                    DoorCheck otherDoor = room.GetComponent<DoorCheck>();
                    if (otherDoor.bottomDoor)
                        topDoorType = 1;
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
                        rightDoorType = 1;
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
                        bottomDoorType = 1;
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
                        leftDoorType = 1;
                    else
                        leftDoorType = 2;
                }
            }
        }
    }
}
