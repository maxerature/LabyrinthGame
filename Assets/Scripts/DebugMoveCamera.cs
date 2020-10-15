using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMoveCamera : MonoBehaviour
{
    public float Speed = 2.5f;

    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        if (Camera.current != null)
        {
            Camera.current.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));
        }
    }
}

