using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingObstacle : MonoBehaviour
{

    public float blockHeight;
    public float blockWidth;


    public Vector3 topLeftCorner;
    public Vector3 topRightCorner;
    public Vector3 bottomLeftCorner;
    public Vector3 bottomRightCorner;
    public bool posChanged;

    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        posChanged = true;

        rt.sizeDelta = new Vector2(blockWidth, blockHeight);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(posChanged)
        {
            posChanged = false;
            Vector3[] v = new Vector3[4];
            rt.GetWorldCorners(v);

            bottomLeftCorner = v[0];
            topLeftCorner = v[1];
            topRightCorner = v[2];
            bottomRightCorner = v[3];
        }
    }
}
