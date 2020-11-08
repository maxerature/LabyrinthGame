using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerShoot : MonoBehaviour
{

    //public GameObject pistol_flash;
    public Animator pistolDirection;

    // Start is called before the first frame update
    void Start()
    {
        pistolDirection = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = GetMouseWorldPosition();
        Vector3 aimDir = (targetPosition - transform.position).normalized;

        Vector3 vectorToTarget = targetPosition - transform.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        //change direction pistol is facing
        pistolDirection.SetFloat("MousePositionHoriz", aimDir.x);
        pistolDirection.SetFloat("MousePositionVert", aimDir.y);
        
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

}
