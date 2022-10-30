using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class XPBar : MonoBehaviour
{
    public int level;

    public float currentXP;

    public float requiredXP;

    private float lerpTimer;

    private float delayTimer;

    [Header("UI")] 
    public Image frontXPBar;
    public Image backXPBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    [Header("Multipliers")] 
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f,4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;
    
    
    // Start is called before the first frame update
    void Start()
    {
        frontXPBar.fillAmount = currentXP / requiredXP;
        backXPBar.fillAmount = currentXP / requiredXP;
        requiredXP = CalculateRequiredXP();
        levelText.text = "Level " + level;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXPUI();
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            GainExperienceFlatRate(20);
        }

        if (currentXP > requiredXP)
        {
            LevelUp();
        }
    }
    
    public void UpdateXPUI()
    {
        float xpFraction = currentXP / requiredXP; // decimal value to hold the xp in decimal
        float FrontXP = frontXPBar.fillAmount;
        if (FrontXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXPBar.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXPBar.fillAmount = Mathf.Lerp(FrontXP, backXPBar.fillAmount, percentComplete);
            }
        }

        xpText.text = currentXP + "/" + requiredXP;
    }

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

    public void LevelUp()
    {
        level++;
        frontXPBar.fillAmount = 0f;
        backXPBar.fillAmount = 0f;
        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        GetComponent<PlayerExperience>().IncreaseHealth(level);
        requiredXP = CalculateRequiredXP();
        levelText.text = "Level " + level;
    }

    private int CalculateRequiredXP()
    {
        int solveForRequiredXP = 0;
        for (int levelCycle =1 ; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXP += (int)Math.Floor(levelCycle + additionMultiplier * Math.Pow(powerMultiplier, levelCycle / divisionMultiplier));
            
        }
        return solveForRequiredXP / 4;
    }
}
