using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using TMPro;
public class CurrencySystem : MonoBehaviour
{
    public int cash;

    public float currentCash;

    public float requiredCash;

    private float lerpTimer;
    private float delayTimer;
    [Header("UI")] public Image frontCashBar;
    [Header("UI")] public Image backCashBar;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI currentCashText;
    [Header("Multipliers")]
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f,4f)]
    public float powerMultiplier = 2;
    [Range(7f,14f)]
    public float divisionMultiplier = 7;
    // Start is called before the first frame update
    void Start()
    {
        frontCashBar.fillAmount = currentCash / requiredCash;
        backCashBar.fillAmount = currentCash / requiredCash;
        requiredCash = CalculateRequiredCash();
        moneyText.text = "Cash" + cash;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCashUI();
        if (Input.GetKeyDown(KeyCode.M))
        {
            GainCashFlatRate(50);
        }

        if (currentCash > requiredCash)
        {
            CashUP();
        }
    }

    public void UpdateCashUI()
    {
        float cashFraction = currentCash / requiredCash;
        float FCash = frontCashBar.fillAmount;
        if (FCash < cashFraction)
        {
            delayTimer += Time.deltaTime;
            backCashBar.fillAmount = cashFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentageComplete = lerpTimer / 4;
                frontCashBar.fillAmount = Mathf.Lerp(FCash, backCashBar.fillAmount, percentageComplete);
            }
        }

        moneyText.text = currentCash + "/" + requiredCash;
    }

    public void GainCashFlatRate(float cashGained)
    {
        currentCash += cashGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    public void GainCashFlatRateScalable(float cashGained, int PassedCash)
    {
        if (cashGained < cash)
        {
            float multiplier = 1 + (cash - PassedCash) * 0.1f;
            currentCash += cashGained * multiplier;
        }
        else
        {
            currentCash += cashGained;
        }

        lerpTimer = 0f;
        delayTimer = 0f;
    }
    

    public void CashUP()
    {
        cash++;
        frontCashBar.fillAmount = 0f;
        backCashBar.fillAmount = 0f;
        currentCash = Mathf.RoundToInt(currentCash - requiredCash);
        GetComponent<CurrencyBank>().IncreaseCash(cash);
        requiredCash = CalculateRequiredCash();
        moneyText.text = "Cash" + cash;
    }

    private int CalculateRequiredCash()
    {
        int solveForRequiredCash = 0;
        for (double CashCycle = 1; CashCycle <= cash; CashCycle++)
        {
            solveForRequiredCash += (int) Mathf.Floor((float) (CashCycle + additionMultiplier * Math.Pow(powerMultiplier, CashCycle / divisionMultiplier)));
        }
        return solveForRequiredCash / 4;
    }
    
}
