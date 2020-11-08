using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        int index = Random.Range(0, sprites.Length);
        spr.sprite = sprites[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
