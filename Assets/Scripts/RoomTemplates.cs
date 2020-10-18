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
    public bool spawnedBoss;
    public GameObject boss;
    public GameObject spawn;


    void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            Instantiate(boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
            Instantiate(spawn, rooms[0].transform.position, Quaternion.identity);
            Debug.Log("Boss Spawned!");
            spawnedBoss = true;

        } else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
