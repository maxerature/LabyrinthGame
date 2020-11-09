using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject[] openBottomRooms;
    public GameObject[] openTopRooms;
    public GameObject[] openLeftRooms;
    public GameObject[] openRightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;
    public GameObject spawn;

    void Start()
    {
        spawnedBoss = false;
        //Invoke("GenerateRoomsLoop", 5f);
        Invoke("Deactivate", 10f);
    }

    void Deactivate()
    {
        for(int i=1; i<rooms.Count; i++)
        {
            rooms[i].SetActive(false);
        }
    }

    void Update()
    {
        if(waitTime <= 0 && !spawnedBoss)
        {
            Instantiate(boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
            Instantiate(spawn, rooms[0].transform.position, Quaternion.identity);
            Debug.Log("Boss Spawned!");
            spawnedBoss = !spawnedBoss;

        } else
        {
            waitTime -= Time.deltaTime;
        }
    }

    void GenerateRoomsLoop()
    {
        foreach(GameObject room in rooms)
        {
            Transform roomCheck = room.transform.Find("DoorCheck");
            DoorCheck dc = roomCheck.gameObject.GetComponent<DoorCheck>();
            dc.checkDoors();
        }
        foreach (GameObject room in rooms)
        {
            Transform roomCheck = room.transform.Find("DoorCheck");
            DoorCheck dc = roomCheck.gameObject.GetComponent<DoorCheck>();
            dc.spawnDoors();
        }
        Deactivate();
    }
}
