using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public GameObject currentRoom;
    public DoorCheck doorCheck;
    // Start is called before the first frame update
    void Start()
    {
        currentRoom = transform.parent.transform.gameObject;
        doorCheck = currentRoom.transform.Find("DoorCheck").GetComponent<DoorCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(transform.position.x-currentRoom.transform.position.x > 0)
        {
            doorCheck.rightRoom.SetActive(true);
        }
        Debug.Log("Collided!");
        if (col.transform.parent.transform.gameObject.tag == "Player")
        {
            GameObject player = col.transform.parent.transform.gameObject;
            player.transform.position = transform.position + transform.TransformDirection(new Vector3(0, -5, 0));
        }
    }
}
