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
        currentRoom = gameObject.transform.parent.transform.gameObject.transform.parent.transform.gameObject;
        doorCheck = currentRoom.GetComponent<DoorCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent.transform.gameObject.tag == "Player")
        {
            GameObject player = col.transform.parent.transform.gameObject;
            if (transform.position.x - currentRoom.transform.position.x > 0)
            {
                doorCheck.rightRoom.SetActive(true);
                player.transform.Translate(new Vector3(5, 0, 0));
                Camera.main.transform.Translate(20, 0, 0);
            }
            else if (transform.position.x - currentRoom.transform.position.x < 0)
            {
                doorCheck.leftRoom.SetActive(true);
                player.transform.Translate(new Vector3(-5, 0, 0));
                Camera.main.transform.Translate(-20, 0, 0);
            }
            else if (transform.position.y - currentRoom.transform.position.y > 0)
            {
                doorCheck.topRoom.SetActive(true);
                player.transform.Translate(new Vector3(5, 0, 0));
                Camera.main.transform.Translate(0, 20, 0);
            }
            else if (transform.position.x - currentRoom.transform.position.x < 0)
            {
                doorCheck.bottomRoom.SetActive(true);
                player.transform.Translate(new Vector3(-5, 0, 0));
                Camera.main.transform.Translate(0, -20, 0);
            }
        }
    }
}
