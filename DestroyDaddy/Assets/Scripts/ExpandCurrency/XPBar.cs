using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class XPBar : MonoBehaviour
{
    public static int level;
    public static float currentXP;
    public static float requiredXP;
    private float lerpTimer;
    private float delayTimer;

    [Header("UI")] 
    public Image frontXPBar;
    public Image backXPBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    [Header("Multipliers")] 
    [Range(1f,300f)]
    public float additionMultiplier = 300; // multiplier for the amount of xp added
    [Range(2f,4f)]
    public float powerMultiplier = 2; // multiplier for the power of the xp bar
    [Range(7f, 14f)]
    public float divisionMultiplier = 7; // multiplier for the division of the xp bar
    
    
    // Start is called before the first frame update
    void Start()
    {
        frontXPBar.fillAmount = currentXP / requiredXP;
        backXPBar.fillAmount = currentXP / requiredXP;
        requiredXP = CalculateRequiredXP(); // calculate the required xp for the next level
        levelText.text = "Level " + level; // set the level text
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXPUI();
        
        //GainExperienceFlatRate(20);
        

        if (currentXP > requiredXP) // check to see when the player needs to level up
        {
            LevelUp();
        }
    }
    
    public void UpdateXPUI()
    {
        float xpFraction = currentXP / requiredXP; // decimal value to hold the xp in decimal
        float FrontXP = frontXPBar.fillAmount; // fill amount for front xp bar
        if (FrontXP < xpFraction) // check if player has gained xp and update the UI
        {
            delayTimer += Time.deltaTime; // delay timer
            backXPBar.fillAmount = xpFraction; // back xp bar 
            if (delayTimer > 3) // 
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4; // animation takes 4 seconds to complete
                frontXPBar.fillAmount = Mathf.Lerp(FrontXP, backXPBar.fillAmount, percentComplete);
            }
        }

        xpText.text = currentXP + "/" + requiredXP;
    }
    /*
     * Function to gain xp
     * @param xp - amount of xp to gain
     */
    public void GainExperienceFlatRate(float xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0f;
    }

    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        if (passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel) * 0.1f;
            currentXP += xpGained * multiplier;
        }
        else
        {
            currentXP += xpGained;
        }
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    /*
     * Function to calculate the required xp for the next level
     * 
     */
    public void LevelUp()
    {
        level++; // increment level
        frontXPBar.fillAmount = 0f; // reset front xp bar
        backXPBar.fillAmount = 0f; // reset back xp bar
        currentXP = Mathf.RoundToInt(currentXP - requiredXP); // take remaining xp and carry it over
        GetComponent<PlayerExperience>().IncreaseHealth(level); // increment player health based on the level
        requiredXP = CalculateRequiredXP(); // calculate the required xp for the next level
        levelText.text = "Level " + level; // update the level text
    }
    /*
     * Function to calculate the required xp for the next level
     */
    private int CalculateRequiredXP()
    {
        int solveForRequiredXP = 0;
        // loop for has many times the player has leveled up
        for (int levelCycle = 1 ; levelCycle <= level; levelCycle++)
        {
            // math function to calculate the required xp 1/4 - 1 +300 (2 ^ 1/4-1 /7)
            solveForRequiredXP += (int)Math.Floor(levelCycle + additionMultiplier * Math.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXP / 4;
    }
}
