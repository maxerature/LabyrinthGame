using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public int itemType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setType(int itemType)
    {
        this.itemType = itemType;
        switch (itemType)
        {
            case 0: //Moderate regeneration on kill
                gameObject.name = "Vampire Bullets";
                break;
            case 1: //Speed boost
                gameObject.name = "Normal Boots";
                break;
            case 2: //Reduce regen timer on damage
                gameObject.name = "Leech Seed";
                break;
            case 3: //Health increase
                gameObject.name = "Glowing Blue Mold";
                break;
            case 4: //Regen timer decrease
                gameObject.name = "Petrified Skull Charm";
                break;
            case 5: //Range increase
                gameObject.name = "Energized Gunpowder";
                break;
            case 6: //Damage Increase
                gameObject.name = "Steelbone Bullets";
                break;
            case 7: //Knockback Increase
                gameObject.name = "Power Jelly";
                break;
            case 8: //Regen ammount
                gameObject.name = "Structured Skin";
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
