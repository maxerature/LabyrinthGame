using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //Children
    public Text healthText;
    public Text rangeText;
    public Text damText;
    public Text knockbackText;
    public Text regenText;
    public Text killRegenText;
    public Text damRegenText;

    //Components
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealthText(float cur, float max)
    {
        string text = cur + "/" + max;
        healthText.text = text;
    }

    public void updateTexts(float rangeMod, float damMod, float kbMod, float regenMod, float killRegenMod, float damRegenMod)
    {
        rangeText.text = "Range        Modifier: " + rangeMod;
        damText.text = "Damage     Modifier: " + damMod;
        knockbackText.text = "Knockback Modifier: " + kbMod;
        regenText.text = "Regen        Modifier: " + regenMod;
        killRegenText.text = "Regen      on      Kill: " + killRegenMod;
        damRegenText.text = "Damage Timer Dec: " + damRegenMod;
    }
}
