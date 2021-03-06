﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public int itemType;
    public GameObject spriteObject;
    public Sprite[] sprites;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = spriteObject.GetComponent<SpriteRenderer>();
        setType(itemType);
    }

    public void setType(int itemType)
    {
        this.itemType = itemType;
        sr = spriteObject.GetComponent<SpriteRenderer>();
        switch (itemType)
        {
            case 0: //Moderate regeneration on kill
                gameObject.name = "Vampire Bullets";
                sr.sprite = sprites[0];
                break;
            case 1: //Speed boost
                gameObject.name = "Normal Boots";
                sr.sprite = sprites[1];
                break;
            case 2: //Reduce regen timer on damage
                gameObject.name = "Leech Seed";
                sr.sprite = sprites[2];
                break;
            case 3: //Health increase
                gameObject.name = "Glowing Blue Mold";
                sr.sprite = sprites[3];
                break;
            case 4: //Regen timer decrease
                gameObject.name = "Petrified Skull Charm";
                sr.sprite = sprites[4];
                break;
            case 5: //Range increase
                gameObject.name = "Energized Gunpowder";
                sr.sprite = sprites[5];
                break;
            case 6: //Damage Increase
                gameObject.name = "Steelbone Bullets";
                sr.sprite = sprites[6];
                break;
            case 7: //Knockback Increase
                gameObject.name = "Power Jelly";
                sr.sprite = sprites[7];
                break;
            case 8: //Regen ammount
                gameObject.name = "Structured Skin";
                sr.sprite = sprites[8];
                break;
        }
    }

    //Stat boost
    public void onPickup(GameObject player, Player playerData)
    {
        //Get count
        int itemCount = 0;
        foreach(string item in playerData.items)
        {
            if (item == gameObject.name)
                itemCount++;
        }

        //Item-specific
        switch (itemType)
        {
            case 0: //Moderate regeneration on kill
                playerData.killRegen = 5f + (itemCount - 1) * 2f;
                break;
            case 1: //Speed boost
                playerData.moveSpeed = playerData.baseMoveSpeed + 3f + (itemCount - 1);
                break;
            case 2: //Reduce regen timer on damage
                playerData.damTimerDec = 1f + (itemCount - 1) * 0.33f;
                break;
            case 3: //Health increase
                playerData.maxHealth = playerData.baseMaxHealth + 25f + (itemCount - 1) * 10f;
                break;
            case 4: //Regen timer decrease
                playerData.regenTimer = playerData.baseRegenTimer - 5f - (itemCount - 1) * 2f;
                break;
            case 5: //Range increase
                playerData.range = playerData.baseRange + 2f + (itemCount - 1) * 0.75f;
                break;
            case 6: //Damage Increase
                playerData.damage = playerData.baseDamage + 0.5f + (itemCount - 1) * 0.2f;
                break;
            case 7: //Knockback Increase
                playerData.knockback = playerData.baseKnockback + 0.1f +(itemCount - 1) * 0.033f;
                break;
            case 8: //Regen ammount
                playerData.regenRate = playerData.baseRegenRate + 0.25f + (itemCount - 1) * 0.1f;
                break;
        }
        Destroy(gameObject);
    }
}
