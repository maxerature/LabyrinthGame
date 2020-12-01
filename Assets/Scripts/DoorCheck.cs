using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    //Prefabs
    public GameObject lockedDoor;
    public GameObject closedDoor;
    public GameObject openDoorPrefab;
    public GameObject itemPrefab;
    public GameObject pitPrefab;
    public GameObject[] pillarPrefabs;
    public GameObject[] enemyTypes;
    public GameObject bossPrefab;

    //Spawn Locations
    public bool topDoor;
    public bool rightDoor;
    public bool bottomDoor;
    public bool leftDoor;

    //Spawn Checks
    public bool topDoorSpawned;
    public bool rightDoorSpawned;
    public bool bottomDoorSpawned;
    public bool leftDoorSpawned;

    // Type of door.  0=none, 1=normal, 2=locked
    public int topDoorType;
    public int rightDoorType;
    public int bottomDoorType;
    public int leftDoorType;

    //Components
    public List<GameObject> enemies;
    public List<GameObject> doors;
    public List<GameObject> unlockedDoors;
    private Collider2D collide;

    //SFX Components
    public AudioSource audioSource;
    public AudioClip doorOpenSFX;
    public AudioClip unlockDoorSFX;
    public AudioClip doorCloseSFX;
    public MusicManager playBossMusic;

    //Reference to surrounding rooms.
    public GameObject topRoom;
    public GameObject rightRoom;
    public GameObject bottomRoom;
    public GameObject leftRoom;

    //Enemy spawning and stats
    public bool safeRoom;
    public bool itemRoom;
    public int enemyCount;

    //If doors are opened
    private bool opened;
    public bool bossRoom;

    private bool[] xPos = new bool[15];
    private bool[] yPos = new bool[15];

    // Start is called before the first frame update
    void Start()
    {
        collide = gameObject.GetComponent<Collider2D>();
        opened = true;
        Invoke("checkDoors", 6f);
        Invoke("spawnDoors", 6.5f);
        Invoke("spawnEnemies", 7.5f);
        if (!safeRoom)
            Invoke("spawnObstacles", 6f);
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

    //Spawn obstacles in the room
    void spawnObstacles()
    {
        if (!bossRoom)
        {
            Vector3 pos;
            int type;
            int obstacleCount = Random.Range(0, 75);
            GameObject obstacle;

            
            for (int i = 0; i < obstacleCount; i++)
            {
                pos = new Vector3(Random.Range(-7, 7), Random.Range(-7, 7), 0);
                if (xPos[(int)(pos.x + 7)] && yPos[(int)(pos.y + 7)] ) {
                    continue;
                }
                xPos[(int)(pos.x + 7)] = true;
                yPos[(int)(pos.y + 7)] = true;
                pos = transform.position + pos;

                type = Random.Range(0, 2);  //0 = pit, 1 = pillar
                switch (type)
                {
                    case 0:
                        obstacle = Instantiate(pitPrefab, pos, Quaternion.identity);
                        obstacle.transform.parent = gameObject.transform;
                        break;
                    case 1:
                        int obNum = Random.Range(0, pillarPrefabs.Length);
                        obstacle = Instantiate(pillarPrefabs[obNum], pos, Quaternion.identity);
                        obstacle.transform.parent = gameObject.transform;
                        break;
                }

            }
        }
    }

    //Spawn enemies in the room
    void spawnEnemies()
    {
        if (!safeRoom)
        {
            if (!bossRoom)
            {
                enemyCount = Random.Range(2, 10);
                int randType;

                for (int i = 0; i < enemyCount; i++)
                {
                    randType = Random.Range(0, enemyTypes.Length);
                    GameObject enemy = Instantiate(enemyTypes[randType], transform.position, Quaternion.identity);
                    enemy.transform.parent = gameObject.transform;

                    Vector3 pos = new Vector3(Random.Range(-6, 6), Random.Range(-6, 6), 0);
                    if (xPos[(int)(pos.x + 6)] && yPos[(int)(pos.y + 6)])
                    {
                        continue;
                    }
                    pos = enemy.transform.position + pos;
                    enemy.transform.position = pos;
                    enemies.Add(enemy);
                }
            }
            else
            {
                GameObject enemy = Instantiate(bossPrefab, transform.position, Quaternion.identity);
                enemy.transform.parent = gameObject.transform;
                enemies.Add(enemy);
            }
        }
        opened = false;
    }

    //Called when an enemy is killed
    public void enemyKilled()
    {
        //Remove dead enemy from list (broken, but somewhat useful)
        for(int i=enemies.Count-1; i >= 0; i--)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        //Spawn item in item rooms
        if(enemyCount == 0)
        {
            if (itemRoom)
            {
                for (int i = Random.Range(1, 4); i > 0; i--)
                {
                    if (!(xPos[(int)(0)] && yPos[(int)(0)]))
                    {
                        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                        ItemHandler ih = item.GetComponent<ItemHandler>();
                        ih.setType(Random.Range(0, 8));
                    }
                }
            }
            else
            {
                int itemChance = Random.Range(0, 5);
                Debug.Log(itemChance);
                if (itemChance == 4)
                {
                    if (!(xPos[(int)(0)] && yPos[(int)(0)]))
                    {
                        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                        ItemHandler ih = item.GetComponent<ItemHandler>();
                        ih.setType(Random.Range(0, 8));
                    }
                }
            }
            //door open sound effect playing after enemies are defeated
            audioSource.PlayOneShot(doorOpenSFX);
        }
    }

    //Spawn doors in room
    public void spawnDoors()
    {
        //TOP DOORS
        //Spawn unlocked door
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
        //Spawn locked door
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

        //RIGHT DOORS
        //Spawn unlocked door
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
        //Spawn locked door
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

        //BOTTOM DOORS
        //Spawn unlocked door
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
        //Spawn locked door
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

        //LEFT DOORS
        //Spawn unlocked door
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
        //Spawn locked door
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
        //Destroy collider used by other rooms to check for doors
        Destroy(collide);
    }

    //Check if surrounding rooms have doors to here
    public void checkDoors()
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
                if (otherDoor != null && otherDoor.bottomDoor)
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
                if (otherDoor != null && otherDoor.leftDoor)
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
                if (otherDoor != null && otherDoor.topDoor)
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
                if (otherDoor != null && otherDoor.rightDoor)
                {
                    leftDoorType = 1;
                }
                else
                    leftDoorType = 2;
            }
        }
    }

    public void playDoorCloseSFX()
    {
        MusicManager.instance.audioSource.PlayOneShot(doorCloseSFX);
    }

    public void activateBossMusic()
    {
        if (bossRoom)
        {
            playBossMusic.PlayTrack(MusicManager.Tracks.Boss);
        }
    }
}

