using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerShoot : MonoBehaviour
{

    //public GameObject pistol_flash;
    public Animator pistol;

    // Start is called before the first frame update
    void Start()
    {
        pistol= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pistol.SetTrigger("shoot");
        }
    }

}
