using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    //Normal Room Prefabs
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    //Modular Room Prefabs (Unused)
    public GameObject[] openBottomRooms;
    public GameObject[] openTopRooms;
    public GameObject[] openLeftRooms;
    public GameObject[] openRightRooms;

    //Special Room Prefabs
    public GameObject closedRoom;
    public GameObject boss;
    public GameObject spawn;

    //List of rooms
    public List<GameObject> rooms;

    //Internal use variables
    public float waitTime;
    private bool spawnedBoss;

    public GameObject bossRoom;

    void Start()
    {
        spawnedBoss = false;
        Invoke("Deactivate", 10f);
    }

    //Turn off when all rooms spawned
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
            int bossNum = rooms.Count - 1;
            Instantiate(boss, rooms[bossNum].transform.position, Quaternion.identity);
            bossRoom = rooms[bossNum];
            Instantiate(spawn, rooms[0].transform.position, Quaternion.identity);
            Debug.Log("Boss Spawned!");
            spawnedBoss = !spawnedBoss;
            DoorCheck dc = rooms[bossNum].transform.Find("DoorCheck").gameObject.GetComponent<DoorCheck>();
            dc.bossRoom = true;
        } else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
