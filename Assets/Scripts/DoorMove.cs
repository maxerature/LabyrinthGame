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

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.parent.transform.gameObject == null)
        {
            return;
        }
        if (col.transform.parent.transform.gameObject.tag == "Player")
        {
            GameObject player = col.transform.parent.transform.gameObject;
            Vector3 newPos = player.transform.position;

            if (transform.position.x - currentRoom.transform.position.x > 0)
            {
                doorCheck.rightRoom.SetActive(true);
                doorCheck.playDoorCloseSFX();
                newPos += new Vector3(5, 0, 0);
                player.transform.position = newPos;
                Camera.main.transform.Translate(20, 0, 0);
                currentRoom.transform.parent.gameObject.SetActive(false);
            }
            else if (transform.position.x - currentRoom.transform.position.x < 0)
            {
                doorCheck.leftRoom.SetActive(true);
                newPos += new Vector3(-5, 0, 0);
                player.transform.position = newPos;
                Camera.main.transform.Translate(-20, 0, 0);
                currentRoom.transform.parent.gameObject.SetActive(false);
            }
            else if (transform.position.y - currentRoom.transform.position.y > 0)
            {
                doorCheck.topRoom.SetActive(true);

                newPos += new Vector3(0, 5, 0);
                player.transform.position = newPos;
                Camera.main.transform.Translate(0, 20, 0);
                currentRoom.transform.parent.gameObject.SetActive(false);
            }
            else if (transform.position.y - currentRoom.transform.position.y < 0)
            {
                doorCheck.bottomRoom.SetActive(true);
                newPos += new Vector3(0, -5, 0);
                player.transform.position = newPos;
                Camera.main.transform.Translate(0, -20, 0);
                currentRoom.transform.parent.gameObject.SetActive(false);
            }
            doorCheck.playDoorCloseSFX();
            doorCheck.activateBossMusic();
        }
    }
}
