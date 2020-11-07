using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject pistol_flash;
    public Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animation.SetTrigger("shoot");
            pistol_flash.SetActive(true);
        }

        pistol_flash.SetActive(false);
        
    }
}
