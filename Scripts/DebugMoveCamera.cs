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

        if(Input.GetKey("e"))
        {
            Camera.current.orthographicSize += 0.15f;
        }
        else if(Input.GetKey("q") && Camera.current.orthographicSize > .2f)
        {
            Camera.current.orthographicSize -= 0.15f;
        }
    }
}

