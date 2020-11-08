using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer spr;

    public Vector2 size;

    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        int index = Random.Range(0, sprites.Length);
        spr.sprite = sprites[index];

        //size = spr.size;
        //float scale = size.x *(120f/81f);
        //
        //
        
        float scale = 1f;
        switch(index)
        {
            case 0:
            case 1:
                scale = 2.185f;
                break;
            case 2:
                scale = 1.5f;
                break;
            case 3:
                scale = 1.85f;
                break;
            case 4:
                scale = 0.764f;
                break;
        }
        transform.localScale = new Vector3(scale/20, scale/20, 1f);
        spr.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
